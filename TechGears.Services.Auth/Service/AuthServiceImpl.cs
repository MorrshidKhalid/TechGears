using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechGears.Services.Auth.Data;
using TechGears.Services.Auth.Models.DTO;
using TechGears.Services.Auth.Service.IService;
using TechGears.Services.CustomerAPI.Utility;

namespace TechGears.Services.Auth.Service
{
    public class AuthServiceImpl : TemplateResponse, IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ResponseDTO _responseDTO;

        public AuthServiceImpl(AppDbContext appDbContext, UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _responseDTO = new();
        }

        public async Task<ResponseDTO?> Login(LoginRequest request)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == request.Username.ToLower());

            if (user != null)
            {
                bool isValid = await _userManager.CheckPasswordAsync(user, request.Password);
                var roles = await _userManager.GetRolesAsync(user);
                return isValid ? 
                    Success(new LoginResponse() { User = UserToDTO(user), Token = _jwtTokenGenerator.GenerateToken(user, roles) })
                    : ErrorResponse(result: new LoginResponse() { User = null }, msg: "Password is not valid");
            }

            return ErrorResponse(msg: "User not found");
        }

        public async Task<ResponseDTO?> Register(RegisterRequest request)
        {
            IdentityUser user = new()
            {
                UserName = request.Email,
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded) 
            {
                var userToReturn = _appDbContext.Users.First(u => u.UserName == request.Email);

                UserDTO userDTO = new()
                {
                    Id = userToReturn.Id,
                    Email = userToReturn.Email,
                    PhoneNumber = userToReturn.PhoneNumber
                };

                return Success(userDTO);
            }

            return ErrorResponse(msg: result.Errors.FirstOrDefault().Description);
        }

        // -------------------

        private UserDTO UserToDTO(IdentityUser user) 
            => new() { Id = user.Id, Email = user.Email, PhoneNumber = user.PhoneNumber };
    }
}
