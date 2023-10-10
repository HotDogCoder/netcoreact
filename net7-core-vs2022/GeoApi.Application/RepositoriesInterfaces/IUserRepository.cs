using GeoApi.Domain.Entities;

namespace GeoApi.Application.RepositoriesInterfaces
{
    public interface IUserRepository
    {
        Task<User> FindByUsernameAsync(string username);
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);

        Task<bool> UserExists(string username);
        Task CreateUser(User user);
    }
}
