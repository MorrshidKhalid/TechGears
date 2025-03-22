using Microsoft.EntityFrameworkCore;
using TechGears.Services.LeadManagmentAPI.Data;
using TechGears.Services.LeadManagmentAPI.Models;
using TechGears.Services.LeadManagmentAPI.Models.DTO;
using TechGears.Services.LeadManagmentAPI.Services.IService;

namespace TechGears.Services.LeadManagmentAPI.Services
{
    public class LeadServiceImpl : ILeadService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ResponseDTO _responseDTO;

        public LeadServiceImpl(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _responseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO?> CreateAsync(InsertUpdateLead insert)
        {
            var newLead = new Lead()
            {
                FirstName = insert.FirstName,
                LastName = insert.LastName,
                Email = insert.Email,
                Phone = insert.Phone,
                Source = insert.Source,
                Status = insert.Status,
                CompanyName = insert.CompanyName,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now),
                AssignedTo = insert.AssignedTo,
            };

            await _appDbContext.AddAsync(newLead);
            await _appDbContext.SaveChangesAsync();

            if (newLead.LeadId > 0)
                return Success(result: newLead);
            else
                return ErrorResponse(msg: "Couldn't create new lead");
        }

        public async Task<ResponseDTO?> DeleteAsync(int leadId)
        {
            if (leadId <= 0)
                return ErrorResponse(msg: $"Invalid id ({leadId})");

            Lead? selectedLead = await Find(leadId);
            if (selectedLead != null)
            {
                _appDbContext.Remove(selectedLead);
                await _appDbContext.SaveChangesAsync();
                return Success(null, msg: $"Lead with id ({leadId} deleted)");
            }

            return ErrorResponse(msg: $"Lead with id ({leadId} is not exists)");
        }

        public async Task<ResponseDTO?> GetAllLeadsAsync()
        {
            if (!_appDbContext.Leads.Any())
                return ErrorResponse(msg: "No leads to show");

            var result = await _appDbContext.Leads.Select(lead => LeadToDTO(lead)).ToListAsync();

            return Success(result);
        }

        public async Task<ResponseDTO?> GetLeadByIdAsync(int leadId)
        {
            if (leadId <= 0)
                return ErrorResponse(msg: $"Invalid id ({leadId})");

            Lead? selectedLead = await Find(leadId);

            return Success(
                LeadToDTO(selectedLead), 
                msg:(selectedLead != null) ? "Found." : $"Lead with id ({leadId}) not found.");
        }

        public async Task<ResponseDTO?> UpdateAsync(int leadId, InsertUpdateLead update)
        {
            if (leadId <= 0)
                return ErrorResponse(msg: $"Invalid id ({leadId})");

            Lead? selectedLead = await _appDbContext.Leads.SingleAsync(l => l.LeadId == leadId);

            if (selectedLead != null)
            {
                selectedLead.FirstName = update.FirstName;
                selectedLead.LastName = update.LastName;
                selectedLead.CompanyName = update.CompanyName;
                selectedLead.Email = update.Email;
                selectedLead.Phone = update.Phone;
                selectedLead.Indestry = update.Indestry;
                selectedLead.Source = update.Source;
                selectedLead.Status = update.Status;
                selectedLead.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

                await _appDbContext.SaveChangesAsync();
                return Success(LeadToDTO(selectedLead), msg: "Updated");
            }

            return ErrorResponse(msg: $"lead with id ({leadId}) not found");
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
        private LeadDTO LeadToDTO(Lead l) => new()
        {
            FirstName = l.FirstName,
            LastName = l.LastName,
            CompanyName = l.CompanyName,
            Email = l.Email,
            Phone = l.Phone,
            Indestry = l.Indestry,
            Source = l.Source,
            Status = l.Status
        };
    }
}
