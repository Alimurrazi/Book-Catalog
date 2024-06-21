using BookStore.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> SignupAsync(SignUpModel signUpModel);
        Task<string> SigninAsync(User user, SignInModel signinModel);
        Task<StorageToken> GetRefreshToken(string token);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserAsync(string email);
        Task RevokeRefreshToken(string token);
        Task SaveRefreshToken(string userId, string token, DateTime expires);
    }
}
