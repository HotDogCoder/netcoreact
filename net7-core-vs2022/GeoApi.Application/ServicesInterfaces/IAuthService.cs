using System.Threading.Tasks;

namespace GeoApi.Application.ServicesInterfaces
{
    public interface IAuthService
    {
        Task<bool> Register(string username, string password);
        Task<string> Login(string username, string password);
        Task<string> RefreshToken(string refreshToken);
        Task<bool> VerifyToken(string token);
    }
}
