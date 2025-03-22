using TechGears.Services.LeadManagmentAPI.Models.DTO;

namespace TechGears.Services.LeadManagmentAPI.Services.IService
{
    // Handle Conversion.
    public interface ILeadConversionService
    {
        Task<ResponseDTO?> ConvertLeadToCustomer(int leadId);
    }
}
