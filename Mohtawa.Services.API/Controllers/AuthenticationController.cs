
using Microsoft.AspNetCore.Mvc;
using Mohtawa.Services.Application.Interfaces;
using Mohtawa.Services.Application.Models.Requests.Authentication;

namespace Mohtawa.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authenticationService.Login(request);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var response = await _authenticationService.Register(request);
            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
