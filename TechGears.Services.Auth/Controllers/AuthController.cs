using Microsoft.AspNetCore.Mvc;
using TechGears.Services.Auth.Models.DTO;
using TechGears.Services.Auth.Service.IService;

namespace TechGears.Services.Auth.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAssignRole _assignRole;

        public AuthController(IAuthService authService, IAssignRole assignRole)
        {
            _authService = authService;
            _assignRole = assignRole;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            ResponseDTO? response = await _authService.Register(model);

            if (response != null && !response.IsSuccess)
                return BadRequest(response);

            else return Ok(response);
        }
        

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            ResponseDTO? loginResponse = await _authService.Login(model);

            if (loginResponse != null && !loginResponse.IsSuccess)
                return BadRequest(loginResponse);

            return Ok(loginResponse);
        }
        

        [HttpPost("assignrole/{role}")]
        public async Task<IActionResult> AssignRole(string role, [FromBody] LoginRequest model)
        {
            var loginResponse = await _assignRole.AssignRole(model.Username, role);

            if (loginResponse != null && !loginResponse.IsSuccess)
                return BadRequest(loginResponse);

            return Ok(loginResponse);
        }
    }
}
