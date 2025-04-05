using TechGears.Services.CustomerAPI.Models.DTO;

namespace TechGears.Services.CustomerAPI.Service.IService
{
    public interface IUserService
    {
        Task<bool> IsUserExists(string? username);
    }
}