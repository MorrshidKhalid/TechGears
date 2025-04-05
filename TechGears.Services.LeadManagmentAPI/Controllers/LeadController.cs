using TechGears.Services.LeadManagmentAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using TechGears.Services.LeadManagmentAPI.Service.IService;

namespace TechGears.Services.LeadManagmentAPI.Controllers
{
    [ApiController]
    [Route("api/leads/")]
    public class LeadController : ControllerBase
    {

        private readonly ILeadService _leadService;
        private readonly ILeadConversionService _leadConversion;
        private readonly ILeadAssignmentService _leadAssignment;

        public LeadController(ILeadService leadService, ILeadConversionService leadConversion, ILeadAssignmentService leadAssignment)
        {
            _leadService = leadService;
            _leadConversion = leadConversion;
            _leadAssignment = leadAssignment;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateLeadAsync([FromBody] InsertUpdateLead model)
        {
            var createdLead = await _leadService.CreateAsync(model);

            if (createdLead != null && !createdLead.IsSuccess)
                return BadRequest(createdLead);

            return Ok(createdLead);
        }


        [HttpGet("all")]
        public async Task<IActionResult> AllAsync()
        {
            ResponseDTO? response = await _leadService.GetAllLeadsAsync();

            if (response == null || !response.IsSuccess)
            {
                return NotFound(response?.Message);
            }

            return Ok(response);
        }


        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var selectedLead = await _leadService.GetLeadByIdAsync(id);

            if (!selectedLead.IsSuccess)
                return BadRequest(selectedLead);

            return Ok(selectedLead);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, InsertUpdateLead model)
        {
            var selectedLead = await _leadService.UpdateAsync(id, model);

            if (!selectedLead.IsSuccess)
                return BadRequest(selectedLead);

            return Ok(selectedLead);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _leadService.DeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("convert")]
        public async Task<IActionResult> ConvertLeadToCustomer([FromBody] LeadToCustomer model)
        {
            ResponseDTO? response = await _leadConversion.ConvertLeadToCustomer(model);

            if (response == null || !response.IsSuccess)
                return BadRequest(response?.Message);

            return Ok(response);
        }


        [HttpPost("assign/{leadId}/{username}")]
        public async Task<IActionResult> AssignLeadToSalesRep(int leadId, string username)
        {
            ResponseDTO? response = await _leadAssignment.AssignLeadToSalesRep(leadId, username);

            if (response == null || !response.IsSuccess)
                return NotFound(response?.Message);

            return Ok(response);
        }
    }
}