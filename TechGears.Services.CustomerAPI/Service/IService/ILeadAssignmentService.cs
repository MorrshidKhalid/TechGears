using TechGears.Services.CustomerAPI.Models.DTO;

namespace TechGears.Services.CustomerAPI.Service.IService
{
    // Handle Assignment.
    public interface ILeadAssignmentService
    {
        Task<ResponseDTO?> AssignLeadToSalesRep(int leadId, int salesRepId);
        Task<ResponseDTO?> ChangeLeadStatus(int leadId, int status);
    }
}
