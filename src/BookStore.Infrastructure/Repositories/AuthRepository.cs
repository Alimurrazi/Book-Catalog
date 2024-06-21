using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Store;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signinManager;
        private readonly IConfiguration configuration;
        private readonly InMemoryTokenStore inMemoryTokenStore;
        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, InMemoryTokenStore inMemoryTokenStore)
        {
            this.userManager = userManager;
            this.signinManager = signInManager;
            this.configuration = configuration;
            this.inMemoryTokenStore = inMemoryTokenStore;
        }

        public async Task<IdentityResult> SignupAsync(SignUpModel signUpModel)
        {
            var user = new User()
            {
                UserName = signUpModel.UserName,
                Email = signUpModel.Email,
            };
            var result = await userManager.CreateAsync(user, signUpModel.Password);
            return result;
        }

        public async Task<User> GetUserAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user;
        }

        private JwtSecurityToken GetTokenPayload(SignInModel signInModel)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInModel.Email),
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: authClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return token;
        }

        public async Task<string> SigninAsync(User user, SignInModel signInModel)
        {
            var result = await signinManager.PasswordSignInAsync(user, signInModel.Password, true, false);
            if(!result.Succeeded)
            {
                return null;
            }

            var token = GetTokenPayload(signInModel);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<StorageToken> GetRefreshToken(string token)
        {
            var refreshToken = await inMemoryTokenStore.GetRefreshTokenAsync(token);
            return refreshToken;
        }

        public async Task RevokeRefreshToken(string token)
        {
            await inMemoryTokenStore.RevokeRefreshTokenAsync(token);
        }

        public async Task SaveRefreshToken(string userId, string token, DateTime expires)
        {
            await inMemoryTokenStore.SaveRefreshTokenAsync(userId, token, expires);
        }

    }
}
