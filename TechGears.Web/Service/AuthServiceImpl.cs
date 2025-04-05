using TechGears.Web.Models.AuthModels;
using TechGears.Web.Models;
using TechGears.Web.Service.IService;
using static TechGears.Web.Utility.SD;

namespace TechGears.Web.Service
{
    public class AuthServiceImpl : IAuthService
    {
         // First portion of a base url.
        private readonly string FPU = "/api/auth/";
        
        private readonly IBaseService _baseService;

        public AuthServiceImpl(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> LoginAsync(LoginRequest request)
        {
            
            return await _baseService
                .SendAsync(
                  new Request()
                  {
                    ApiType = ApiType.POST,
                     Url = $"{AuthAPIBase}{FPU}login",
                    Data = request
                  }  
                );
        }


        public async Task<ResponseDTO?> RegisterAsync(RegisterRequest request)
        {
            return await _baseService
                .SendAsync(
                  new Request()
                  {
                    ApiType = ApiType.POST,
                    Url = $"{AuthAPIBase}{FPU}register",
                    Data = request
                  }  
                );
        }
    }
}