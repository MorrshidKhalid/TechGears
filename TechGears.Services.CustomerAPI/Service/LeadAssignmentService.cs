using Microsoft.EntityFrameworkCore;
using TechGears.Services.CustomerAPI.Data;
using TechGears.Services.CustomerAPI.Models;
using TechGears.Services.CustomerAPI.Models.DTO;
using TechGears.Services.CustomerAPI.Service.IService;



namespace TechGears.Services.CustomerAPI.Service
{
     public class LeadAssignmentService : ILeadAssignmentService
     {

        private readonly AppDbContext _appDbContext;
        private readonly ResponseDTO _responseDTO;

        public LeadAssignmentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _responseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO?> AssignLeadToSalesRep(int leadId, int salesRepId)
        {
            if (leadId <= 0)
                return ErrorResponse(msg: $"Invalid id ({leadId})");

            var selectedLead = await Find(leadId);

            if (selectedLead == null)
                 return ErrorResponse(msg: $"Lead with id ({leadId}) not found");

            

            // *** Before we update we need to check if the sales id is exists. ***
            
            selectedLead.AssignedTo = salesRepId;

            await _appDbContext.SaveChangesAsync();
            return Success(salesRepId, msg: $"Assigned to Rep with id ({salesRepId})");
        }

        public async Task<ResponseDTO?> ChangeLeadStatus(int leadId, int status)
        {

            if (leadId <= 0)
                return ErrorResponse(msg: $"Invalid id ({leadId})");

            var selectedLead = await Find(leadId);

            if (selectedLead == null)
                 return ErrorResponse(msg: $"Lead with id ({leadId}) not found");

            selectedLead.Status = (Status)status;

            await _appDbContext.SaveChangesAsync();

            return Success(status, msg: $"The new lead status is ({(Status)status})");
        }



        // -------------------
        private ResponseDTO Success(object? result, bool flag = true, string msg = "Success")
        {
            _responseDTO.IsSuccess = flag;
            _responseDTO.Message = msg;
            _responseDTO.Result = result;

            return _responseDTO;
        }

        private ResponseDTO ErrorResponse(object? result = null, bool flag = false, string msg = "Error")
        {
            _responseDTO.IsSuccess = flag;
            _responseDTO.Message = msg;
            _responseDTO.Result = result;

            return _responseDTO;
        }

        private async Task<Lead?> Find(int leadId) => await _appDbContext.Leads.SingleOrDefaultAsync(lead => lead.LeadId == leadId);


     }
}