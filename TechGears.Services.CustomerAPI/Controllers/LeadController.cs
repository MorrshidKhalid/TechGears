using TechGears.Services.CustomerAPI.Service.IService;
using TechGears.Services.CustomerAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;

namespace TechGears.Services.CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/lead/")]
    public class LeadController : ControllerBase
    {

        private readonly ILeadService _leadService;

        public LeadController(ILeadService leadService) 
        {
            _leadService = leadService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLeadAsync([FromBody] InsertUpdateLead model)
        {
            var createdLead = await _leadService.CreateAsync(model);

            if (!createdLead.IsSuccess)
                return BadRequest(createdLead);

            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> AllAsync()
        {
            ResponseDTO? response = await _leadService.GetAllLeadsAsync();

            if (response == null || !response.IsSuccess)
            {
                return NotFound(response.Message);
            }

            var list = response.Result;

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

    }
}