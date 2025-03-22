using TechGears.Services.CustomerAPI.Models.DTO;

namespace TechGears.Services.CustomerAPI.Service.IService
{
    // Handle Conversion.
    public interface ILeadConversionService
    {
        Task<ResponseDTO?> ConvertLeadToCustomer(int leadId);
    }
}
