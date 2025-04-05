using Microsoft.EntityFrameworkCore;
using TechGears.Services.LeadManagmentAPI.Data;
using TechGears.Services.LeadManagmentAPI.Models;
using TechGears.Services.LeadManagmentAPI.Models.DTO;
using TechGears.Services.LeadManagmentAPI.Service.IService;
using TechGears.Services.LeadManagmentAPI.Utility;

namespace TechGears.Services.CustomerAPI.Service
{
    // Handle Assignment.
    public class LeadAssignmentServiceImpl : TemplateResponse, ILeadAssignmentService
    {

        private readonly IUserService _userService;
        private readonly AppDbContext _appDbContext;

        public LeadAssignmentServiceImpl(IUserService userService, AppDbContext appDbContext)
        {
            _userService = userService;
            _appDbContext = appDbContext;
        }

        public async Task<ResponseDTO?> AssignLeadToSalesRep(int leadId, string salesRepUsername)
        {
            if (leadId <= 0 || string.IsNullOrEmpty(salesRepUsername))
                return ErrorResponse(msg: $"Invalid id ({leadId}) or Invalid username ({salesRepUsername})");

            Lead? lead = await _appDbContext.Leads.FirstOrDefaultAsync(lead => lead.LeadId == leadId);

            if (lead != null)
            {
                bool isExists = await _userService.IsUserExists(salesRepUsername);

                if (!isExists)
                    return ErrorResponse(msg: $"user with username ({salesRepUsername}) not found");

                lead.AssignedTo = salesRepUsername;

                await _appDbContext.SaveChangesAsync();

                return Success(salesRepUsername, msg: "Successfully Assigned");
            }

            return ErrorResponse(msg: $"Lead with id ({leadId}) not found");
        }

        public Task<ResponseDTO?> ChangeLeadStatus(int leadId, int status)
        {
            throw new NotImplementedException();
        }
    }
}
