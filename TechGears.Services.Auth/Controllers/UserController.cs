using Microsoft.AspNetCore.Mvc;
using TechGears.Services.Auth.Models.DTO;
using TechGears.Services.Auth.Service.IService;

namespace TechGears.Services.Auth.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("exists/{username}")]
        public async Task<IActionResult> Register(string username)
        {
            ResponseDTO? response = await _userService.FindUserByUsername(username);

            if (response != null && !response.IsSuccess)
                return NotFound(response);

            else return Ok(response);
        }

    }
}
