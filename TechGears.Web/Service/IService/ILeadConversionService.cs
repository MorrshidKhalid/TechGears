using TechGears.Web.Models;

namespace TechGears.Web.Service.IService
{
    // Handle Conversion.
    public interface ILeadConversionService
    {
        Task<ResponseDTO?> ConvertLeadToCustomer(int leadId);
    }
}
