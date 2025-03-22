using TechGears.Services.LeadManagmentAPI.Models.DTO;

namespace TechGears.Services.LeadManagmentAPI.Services.IService
{

    // Handle Assignment.
    public interface ILeadAssignmentService
    {
        Task<ResponseDTO?> AssignLeadToSalesRep(int leadId, int salesRepId);
        Task<ResponseDTO?> ChangeLeadStatus(string status);
    }
}
