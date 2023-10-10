using System;
using GeoApi.Domain.Entities;

namespace GeoApi.Application.RepositoriesInterfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken token);
        Task DeleteAsync(RefreshToken token);
    }
}

