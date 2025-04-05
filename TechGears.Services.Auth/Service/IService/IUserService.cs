using TechGears.Services.Auth.Models.DTO;

namespace TechGears.Services.Auth.Service.IService
{
    public interface IUserService
    {
        Task<ResponseDTO> FindUserByUsername(string username);
    }
}