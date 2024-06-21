using BookStore.Application.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> Register(RegisterRequestDto request);
        Task<TokenResponseDto?> Login(LoginRequestDto request);
        Task<TokenResponseDto?> Refresh(RefreshRequestDto request);
    }
}
