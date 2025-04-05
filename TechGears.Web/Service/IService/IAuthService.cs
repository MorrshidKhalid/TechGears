using TechGears.Web.Models.AuthModels;
using TechGears.Web.Models;

namespace TechGears.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDTO?> LoginAsync(LoginRequest request);
        Task<ResponseDTO?> RegisterAsync(RegisterRequest request);
    }
}