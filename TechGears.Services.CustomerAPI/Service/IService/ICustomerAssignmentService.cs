using TechGears.Services.CustomerAPI.Models.DTO;

namespace TechGears.Services.CustomerAPI.Service.IService
{
    public interface ICustomerAssignmentService
    {
        Task<ResponseDTO> AssignTo(string username);
    }
}