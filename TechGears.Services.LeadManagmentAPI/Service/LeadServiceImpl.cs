using Microsoft.EntityFrameworkCore;
using TechGears.Services.LeadManagmentAPI.Data;
using TechGears.Services.LeadManagmentAPI.Models;
using TechGears.Services.LeadManagmentAPI.Models.DTO;
using TechGears.Services.LeadManagmentAPI.Service.IService;
using TechGears.Services.LeadManagmentAPI.Utility;

namespace TechGears.Services.LeadManagmentAPI.Service
{
    public class LeadServiceImpl : TemplateResponse, ILeadService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserService _userService;

        public LeadServiceImpl(AppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
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

            List<LeadDTO> result = await _appDbContext.Leads.Select(lead => LeadToDTO(lead)).ToListAsync();

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

        public async Task<ResponseDTO?> GetLeadByOwner(string username)
        {
            bool isUserValid = await _userService.IsUserExists(username);

            if (string.IsNullOrEmpty(username) || !isUserValid)
                return ErrorResponse(msg: $"please enter a valid username *{username}* is not valid or user is not exists");

            List<LeadDTO> leadsList = await _appDbContext.Leads
                .Where(lead => lead.AssignedTo == username)
                .Select(lead => LeadToDTO(lead)).ToListAsync();

            return Success(result: leadsList);
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
