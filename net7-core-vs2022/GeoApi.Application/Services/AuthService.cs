using System;
using GeoApi.Application.ServicesInterfaces;
using GeoApi.Domain.Entities;
using GeoApi.Application.RepositoriesInterfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GeoApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> Register(string username, string password)
        {
            if (await _userRepository.UserExists(username))
                return false; // User already exists

            // Logic to hash and salt the password (always store hashed passwords!)
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                // other fields as necessary
            };

            await _userRepository.CreateUser(user);
            return true;
        }


        public async Task<string> Login(string username, string password)
        {
            var user = await _userRepository.FindByUsernameAsync(username);

            if (user == null)
            {
                // User not found
                return null;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isValidPassword)
            {
                // Invalid password
                return null;
            }

            return GenerateToken(user);
        }



        public async Task<string> RefreshToken(string refreshToken)
        {
            // 1. Fetch the stored refresh token from the database using the provided token.
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            if (storedToken == null)
            {
                // Token does not exist or has already been used
                return null;
            }

            // 2. Check if the token is still valid.
            if (storedToken.ExpiryDate < DateTime.UtcNow)
            {
                // Token has expired. It's good practice to remove it from the database.
                await _refreshTokenRepository.DeleteAsync(storedToken);
                return null;
            }

            // 3. Fetch the associated user
            var user = await _userRepository.GetByIdAsync(storedToken.UserId);
            if (user == null)
            {
                // Associated user does not exist anymore
                return null;
            }

            // 4. Invalidate the used refresh token by deleting it.
            await _refreshTokenRepository.DeleteAsync(storedToken);

            // 5. (Optional) Create and store a new refresh token for subsequent refreshes.
            var newRefreshToken = GenerateToken(user);
            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = newRefreshToken,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddMonths(1)  // e.g., refresh token valid for one month
            });

            // 6. Generate a new JWT
            return newRefreshToken;
        }


        public Task<bool> VerifyToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // Not validating issuer since it's not provided
                    ValidateAudience = false, // Not validating audience since it's not provided
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}
