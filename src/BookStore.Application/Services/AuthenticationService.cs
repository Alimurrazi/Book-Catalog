using BookStore.Application.Dtos.Auth;
using BookStore.Application.Interfaces;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthRepository authRepository;

        public AuthenticationService(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task<IdentityResult> Register(RegisterRequestDto request)
        {
            SignUpModel signUpModel = new SignUpModel();
            signUpModel.UserName = request.UserName;
            signUpModel.Email = request.Email;
            signUpModel.Password = request.Password;
            return await authRepository.SignupAsync(signUpModel);
        }
    }
}
