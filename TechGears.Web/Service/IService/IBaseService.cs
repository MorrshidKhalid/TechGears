using TechGears.Web.Models;

namespace TechGears.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(Request request);
    }
}