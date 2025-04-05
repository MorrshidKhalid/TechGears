using TechGears.Services.LeadManagmentAPI.Models.DTO;

namespace TechGears.Services.LeadManagmentAPI.Service.IService
{
    // Handle Assignment.
    public interface ILeadAssignmentService
    {
        Task<ResponseDTO?> AssignLeadToSalesRep(int leadId, string salesRepUsername);
        Task<ResponseDTO?> ChangeLeadStatus(int leadId, int status);
    }
}
