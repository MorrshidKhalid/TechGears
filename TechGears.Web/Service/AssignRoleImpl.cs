using TechGears.Web.Models.AuthModels;
using TechGears.Web.Models;
using TechGears.Web.Service.IService;
using static TechGears.Web.Utility.SD;

namespace TechGears.Web.Service
{
    public class AssignRoleImpl : IAssignRole
    {
        // First portion of a base url.
        private readonly string FPU = "/api/auth/";
        
        private readonly IBaseService _baseService;

        public AssignRoleImpl(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> AssignRole(string role, LoginRequest loginRequest)
        {
            return await _baseService
                .SendAsync(
                  new Request()
                  {
                    ApiType = ApiType.POST,
                    Url = $"{AuthAPIBase}{FPU}assignrole/{role}",
                    Data = loginRequest
                  }  
                );
        }
    }
}