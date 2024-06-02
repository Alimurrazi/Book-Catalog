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
    }
}
