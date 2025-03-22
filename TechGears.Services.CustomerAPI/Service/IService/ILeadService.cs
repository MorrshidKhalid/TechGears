using TechGears.Services.CustomerAPI.Models.DTO;

namespace TechGears.Services.CustomerAPI.Service.IService
{
    // Handle simple CRUD.
    public interface ILeadService
    {
        Task<ResponseDTO?> GetAllLeadsAsync();
        Task<ResponseDTO?> GetLeadByIdAsync(int leadId);
        Task<ResponseDTO?> CreateAsync(InsertUpdateLead insert);
        Task<ResponseDTO?> UpdateAsync(int leadId, InsertUpdateLead update);
        Task<ResponseDTO?> DeleteAsync(int leadId);
    }
}