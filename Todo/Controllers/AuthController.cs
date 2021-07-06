using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Security;
using Todo.Security.Options;
using Todo.ViewModel;

namespace Todo.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenGenerator _tokenGenerator;

        public AuthController(SignInManager<IdentityUser> signInManager, TokenGenerator tokenGenerator)
        {
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
                });
            }

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Erros = new List<string>() { "Invalid user or password" }
                });
            }

            var token = _tokenGenerator.GenerateToken();

            return Ok(new
            {
                Success = true,
                Token = token
            });
        }
    }
}
