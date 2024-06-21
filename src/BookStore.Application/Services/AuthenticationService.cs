using BookStore.Application.Dtos.Auth;
using BookStore.Application.Interfaces;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthRepository authRepository;
        private readonly IConfiguration configuration;

        public AuthenticationService(IAuthRepository authRepository, IConfiguration configuration)
        {
            this.authRepository = authRepository;
            this.configuration = configuration;
        }

        public async Task<IdentityResult> Register(RegisterRequestDto request)
        {
            SignUpModel signUpModel = new SignUpModel();
            signUpModel.UserName = request.UserName;
            signUpModel.Email = request.Email;
            signUpModel.Password = request.Password;
            return await authRepository.SignupAsync(signUpModel);
        }

        public async Task<TokenResponseDto?> Login(LoginRequestDto request)
        {
            SignInModel signInModel = new SignInModel();
            signInModel.Email = request.Email;
            signInModel.Password = request.Password;

            var user = await authRepository.GetUserAsync(signInModel.Email);
            if (user == null)
            {
                return null;
            }

            var accessToken = await authRepository.SigninAsync(user, signInModel);
            var refreshToken = GenerateRefreshToken();
            var result = new TokenResponseDto
            {
                RefreshToken = refreshToken,
                AccessToken = accessToken
            };
            await authRepository.SaveRefreshToken(user.Id, refreshToken, DateTime.Now.AddDays(1));

            return result;
        }

        private JwtSecurityToken GetTokenPayload(User user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: authClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<TokenResponseDto?> Refresh(RefreshRequestDto request)
        {
            var existingRefreshToken = await authRepository.GetRefreshToken(request.RefreshToken);
            if (existingRefreshToken == null || existingRefreshToken.IsRevoked) return null;
            var user = await authRepository.GetUserByIdAsync(existingRefreshToken.UserId);
            if (user == null) return null;
            
            var refreshToken = GenerateRefreshToken();
            var accessToken = GetTokenPayload(user);

            await authRepository.SaveRefreshToken(user.Id, refreshToken, DateTime.Now.AddDays(1));
            await authRepository.RevokeRefreshToken(existingRefreshToken.RefreshToken);

            var result = new TokenResponseDto
            {
                RefreshToken = refreshToken,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken)
            };

            return result;
        }
    }
}
