using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly UserManager<User> userManager;
        public AuthRepository(UserManager<User> userManager)
        {
            this.userManager = userManager;
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
    }
}
