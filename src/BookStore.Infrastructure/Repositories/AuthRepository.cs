using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
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
        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signinManager = signInManager;
            this.configuration = configuration;
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

        public async Task<string> SigninAsync(SignInModel signInModel)
        {
            var user = await GetUserAsync(signInModel.Email);
            if(user == null)
            {
                return null;
            }

            var result = await signinManager.PasswordSignInAsync(user, signInModel.Password, true, false);
            if(!result.Succeeded)
            {
                return null;
            }

            var token = GetTokenPayload(signInModel);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
