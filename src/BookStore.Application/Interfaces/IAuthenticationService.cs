using BookStore.Application.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegisterRequestDto request);
    }
}
