using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechGears.Services.Auth.Data;
using TechGears.Services.Auth.Models.DTO;
using TechGears.Services.CustomerAPI.Utility;

namespace TechGears.Services.Auth.Service.IService
{
    public class UserServiceImpl : TemplateResponse, IUserService
    {

        private readonly AppDbContext _appDbContext;

        public UserServiceImpl(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResponseDTO> FindUserByUsername(string username)
        {
            IdentityUser? user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());

            // sent result as (string) to allow deserializ it
            if (user != null)
                return Success(result: "true");

            return ErrorResponse(msg: "User not found");
        }
    }
}