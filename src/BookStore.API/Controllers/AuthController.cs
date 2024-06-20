using BookStore.Application.Dtos.Auth;
using BookStore.Application.Dtos.Book;
using BookStore.Application.Interfaces;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : MainController
    {

        private readonly IAuthenticationService authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Signup(RegisterRequestDto registerRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await authenticationService.Register(registerRequestDto);
            if(result.Succeeded)
            {
                return Ok(result);
            }

            return Unauthorized();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Signin(LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await authenticationService.Login(loginRequestDto);
            if(string.IsNullOrWhiteSpace(result)) return Unauthorized();

            return Ok(result);
        }

    }
}
