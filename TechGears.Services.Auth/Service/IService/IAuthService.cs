using TechGears.Services.Auth.Models.DTO;

namespace TechGears.Services.Auth.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDTO?> Register(RegisterRequest request);
        Task<ResponseDTO?> Login(LoginRequest request);
    }
}
