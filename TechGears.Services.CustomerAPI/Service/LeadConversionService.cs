using Microsoft.EntityFrameworkCore;
using TechGears.Services.CustomerAPI.Data;
using TechGears.Services.CustomerAPI.Models;
using TechGears.Services.CustomerAPI.Models.DTO;
using TechGears.Services.CustomerAPI.Service.IService;


namespace TechGears.Services.CustomerAPI.Service
{
    public class LeadConversionService : ILeadConversionService
    {

        private readonly ResponseDTO _responseDTO;
        
        public LeadConversionService()
        {
            _responseDTO = new();
        }



       public async Task<ResponseDTO?> ConvertLeadToCustomer(int leadId)
        {
            // We need to find if there is a customer in the first place.

            return _responseDTO;
        }
    }
}