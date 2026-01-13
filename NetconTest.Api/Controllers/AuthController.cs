using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetconTest.Api.Auth;
using NetconTest.Api.Swagger.Examples;
using Swashbuckle.AspNetCore.Filters;

namespace NetconTest.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("authorization")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(LoginRequest), typeof(LoginRequestExample))]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = _authService.GenerateToken(request.Name, request.Password);

            if (token == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(new { token });
        }
    }
}
