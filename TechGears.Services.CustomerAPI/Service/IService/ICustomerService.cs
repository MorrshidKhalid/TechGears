using TechGears.Services.CustomerAPI.Models.DTO;

namespace TechGears.Services.CustomerAPI.Service.IService
{
    // Handle simple CRUD.
    public interface ICustomerService
    {
        Task<ResponseDTO?> GetAllCustomersAsync();
        Task<ResponseDTO?> GetCustomerByIdAsync(int customrtId);
        Task<ResponseDTO?> CreateAsync(InsertUpdateCustomer insert);
        Task<ResponseDTO?> UpdateAsync(int customrtId, InsertUpdateCustomer update);
        Task<ResponseDTO?> DeleteAsync(int customrtId);
    }
}