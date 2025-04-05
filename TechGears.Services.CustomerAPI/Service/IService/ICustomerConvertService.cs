using TechGears.Services.CustomerAPI.Models.DTO;

namespace TechGears.Services.CustomerAPI.Service.IService
{
    public interface ICustomerConvertService
    {
        public Task<ResponseDTO> Convert(InsertUpdateCustomer insert);
    }
}