using TechGears.Services.LeadManagmentAPI.Models.DTO;

namespace TechGears.Services.LeadManagmentAPI.Services.IService
{
    public interface ILeadManagmentService
    {
        Task<ResponseDTO> ResponseAsync();
    }
}
