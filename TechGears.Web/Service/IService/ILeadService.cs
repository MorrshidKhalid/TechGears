using TechGears.Web.Models.Lead;
using TechGears.Web.Models;

namespace TechGears.Web.Service.IService
{
    public interface ILeadService
    {
        Task<ResponseDTO?> GetAllLeadsAsync();
        Task<ResponseDTO?> GetLeadByIdAsync(int leadId);
        Task<ResponseDTO?> CreateAsync(InsertUpdateLead insert);
        Task<ResponseDTO?> UpdateAsync(int leadId, InsertUpdateLead update);
        Task<ResponseDTO?> DeleteAsync(int leadId);
    }
}