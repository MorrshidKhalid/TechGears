using TechGears.Web.Models;
using TechGears.Web.Models.Lead;
using static TechGears.Web.Utility.SD;

namespace TechGears.Web.Service.IService
{
    public class LeadServiceImpl : ILeadService
    {

        // First portion of a base url.
        private readonly string FPU = "/api/leads/";

        private readonly IBaseService _baseService;

        public LeadServiceImpl(IBaseService baseService) => _baseService = baseService;

        public async Task<ResponseDTO?> GetAllLeadsAsync()
        {
            return await _baseService.SendAsync(
                new Request() 
                {
                    ApiType = ApiType.GET,
                    Url = $"{CustomerAPIBase}{FPU}all"
                }
            );
        }
        
        public async Task<ResponseDTO?> GetLeadByIdAsync(int leadId) 
        {
            return await _baseService.SendAsync(
                new Request() 
                {
                    ApiType = ApiType.GET,
                    Url = $"{CustomerAPIBase}{FPU}id/{leadId}"
                }
            );
        }

        public async Task<ResponseDTO?> CreateAsync(InsertUpdateLead insert) 
        {
            return await _baseService.SendAsync(
                new Request() 
                {
                    ApiType = ApiType.POST,
                    Url = $"{CustomerAPIBase}{FPU}create",
                    Data = insert
                }
            );
        }

        public async Task<ResponseDTO?> UpdateAsync(int leadId, InsertUpdateLead update)
        {
            return await _baseService.SendAsync(
                new Request() 
                {
                    ApiType = ApiType.PUT,
                    Url = $"{CustomerAPIBase}{FPU}update/{leadId}",
                    Data = update
                }
            );
        }

        public async Task<ResponseDTO?> DeleteAsync(int leadId) 
        {
            return await _baseService.SendAsync(
                new Request() 
                {
                    ApiType = ApiType.DELETE,
                    Url = $"{CustomerAPIBase}{FPU}delete/{leadId}"
                }
            );
        }

    }
}