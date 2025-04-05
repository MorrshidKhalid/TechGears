using TechGears.Services.LeadManagmentAPI.Models.DTO;

namespace TechGears.Services.LeadManagmentAPI.Service.IService
{
    // Handle Conversion.
    public interface ILeadConversionService
    {
        Task<ResponseDTO?> ConvertLeadToCustomer(LeadToCustomer toCustomer);
    }
}
