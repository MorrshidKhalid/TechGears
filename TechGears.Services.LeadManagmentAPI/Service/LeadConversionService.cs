using TechGears.Services.LeadManagmentAPI.Service.IService;
using TechGears.Services.LeadManagmentAPI.Data;
using TechGears.Services.LeadManagmentAPI.Models.DTO;
using TechGears.Services.LeadManagmentAPI.Utility;
using Newtonsoft.Json;
using System.Text;
using TechGears.Services.LeadManagmentAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace TechGears.Services.LeadManagmentAPI.Service
{
    public class LeadConversionService : TemplateResponse, ILeadConversionService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public readonly string FPU = "/api/customers";

        public LeadConversionService(AppDbContext appDbContext, IHttpClientFactory httpClientFactory)
        {
            _appDbContext = appDbContext;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDTO?> ConvertLeadToCustomer(LeadToCustomer toCustomer)
        {

            if (toCustomer.LeadId <= 0)
                return ErrorResponse(msg: $"Lead id ({toCustomer.LeadId}) is not valid");
            
            Lead? toBeConverted = await Find(toCustomer.LeadId);

            if (toBeConverted == null)
                return ErrorResponse(msg: $"Lead with id ({toCustomer.LeadId}) not found");

            HttpClient client = _httpClientFactory.CreateClient("Customer");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");

            message.RequestUri = new Uri($"{SD.CustomerURL}{FPU}/tobeconverted");
            message.Content = new StringContent(JsonConvert
                .SerializeObject(ConvertLeadToCustomer(toBeConverted, toCustomer)), Encoding.UTF8, "application/json");
            message.Method = HttpMethod.Post;

            HttpResponseMessage apiResponse = await client.SendAsync(message);
            
            return await HandelBaseReponse.HandleResponse(apiResponse);
        }

        private async Task<Lead?> Find(int leadId) => await _appDbContext.Leads.SingleOrDefaultAsync(lead => lead.LeadId == leadId);
        
        private LeadToCustomer ConvertLeadToCustomer(Lead lead, LeadToCustomer info)
            => new()
            {
                LeadId = lead.LeadId,
                FirstName = lead.FirstName,
                LastName = lead.LastName,
                CompanyName = lead.CompanyName,
                Email = lead.Email,
                Phone = lead.Phone,
                Indestry = lead.Indestry,
                Address = info.Address,
                AssignedTo = info.AssignedTo
            };
    }
}
