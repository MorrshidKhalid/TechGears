using TechGears.Services.Auth.Models.DTO;

namespace TechGears.Services.Auth.Service.IService
{
    public interface IAssignRole
    {
        Task<ResponseDTO?> AssignRole(string email, string roleName);
    }
}