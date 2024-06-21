using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface ITokenStore
    {
        Task SaveRefreshTokenAsync(string userId, string token, DateTime expires);
        Task<StorageToken> GetRefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(string token);
        Task RemoveExpiredTokensAsync();
    }
}
