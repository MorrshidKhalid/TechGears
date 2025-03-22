using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechGears.Services.Auth.Data;
using TechGears.Services.Auth.Models.DTO;
using TechGears.Services.Auth.Service.IService;

namespace TechGears.Services.Auth.Service
{
    public class AssignRoleImpl : IAssignRole
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbContext;
        private readonly ResponseDTO _responseDTO;

        public AssignRoleImpl(RoleManager<IdentityRole> roleManager, AppDbContext appDbContext, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            _responseDTO = new();
        }

        public async Task<ResponseDTO?> AssignRole(string email, string roleName)
        {
            IdentityUser? user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == email.ToLower());

            if (user == null)
            {
                _responseDTO.Message = "User not found";
                _responseDTO.IsSuccess = false;
                return _responseDTO;
            }

            // Check if role already exists.
            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                // create if dose not exits.
                _roleManager.CreateAsync(new IdentityRole(roleName))
                    .GetAwaiter()
                    .GetResult();

                _responseDTO.Message = $"new role added ({roleName})";
            }

            await _userManager.AddToRoleAsync(user, roleName);
            _responseDTO.IsSuccess = true;
            _responseDTO.Result = user;

            return _responseDTO;
        }
    }
}