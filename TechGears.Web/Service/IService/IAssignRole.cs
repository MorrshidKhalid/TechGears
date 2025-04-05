using TechGears.Web.Models;
using TechGears.Web.Models.AuthModels;

namespace TechGears.Web.Service.IService
{
    public interface IAssignRole
    {
        Task<ResponseDTO?> AssignRole(string role, LoginRequest loginRequest);
    }
}