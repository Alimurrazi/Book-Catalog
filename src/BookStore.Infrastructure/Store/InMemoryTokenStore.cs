using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Store
{
    public class InMemoryTokenStore: ITokenStore
    {
        private readonly ConcurrentDictionary<string, StorageToken> _tokens = new();

        public Task SaveRefreshTokenAsync(string userId, string token, DateTime expires)
        {
            var refreshToken = new StorageToken
            {
                UserId = userId,
                RefreshToken = token,
                Expires = expires,
                IsRevoked = false
            };

            _tokens[token] = refreshToken;
            return Task.CompletedTask;
        }

        public Task<StorageToken> GetRefreshTokenAsync(string token)
        {
            _tokens.TryGetValue(token, out var refreshToken);
            return Task.FromResult(refreshToken);
        }

        public Task RevokeRefreshTokenAsync(string token)
        {
            if (_tokens.TryGetValue(token, out var refreshToken))
            {
                refreshToken.IsRevoked = true;
            }
            return Task.CompletedTask;
        }

        public Task RemoveExpiredTokensAsync()
        {
            var now = DateTime.UtcNow;
            var expiredTokens = _tokens.Values.Where(t => t.Expires <= now).ToList();

            foreach (var expiredToken in expiredTokens)
            {
                _tokens.TryRemove(expiredToken.RefreshToken, out _);
            }

            return Task.CompletedTask;
        }
    }
}
