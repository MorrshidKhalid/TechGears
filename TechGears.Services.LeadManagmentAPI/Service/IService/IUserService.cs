using TechGears.Services.LeadManagmentAPI.Models.DTO;

namespace TechGears.Services.LeadManagmentAPI.Service.IService
{
    public interface IUserService
    {
        Task<bool> IsUserExists(string username);
    }
}