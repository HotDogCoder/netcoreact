using System;
using GeoApi.Application.RepositoriesInterfaces;
using GeoApi.Domain.Entities;
using GeoApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GeoApi.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly GeoApiDbContext _context;

        public RefreshTokenRepository(GeoApiDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        }

        public async Task AddAsync(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RefreshToken token)
        {
            _context.RefreshTokens.Remove(token);
            await _context.SaveChangesAsync();
        }
    }

}

