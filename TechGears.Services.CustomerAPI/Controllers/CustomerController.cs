using Microsoft.AspNetCore.Mvc;
using TechGears.Services.CustomerAPI.Models.DTO;
using TechGears.Services.CustomerAPI.Service.IService;

namespace TechGears.Services.CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/customers/")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerConvertService _convertService;

        public CustomerController(ICustomerService customerService, ICustomerConvertService convertService)
        {
            _customerService = customerService;
            _convertService = convertService;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateCustomer([FromBody] InsertUpdateCustomer model)
        {
            ResponseDTO? response = await _customerService.CreateAsync(model);

            if (response == null || !response.IsSuccess)
                return BadRequest(response?.Message);

            return Ok(response);
        }

        [HttpPost("tobeconverted")]
        public async Task<IActionResult> ToCustomer([FromBody] InsertUpdateCustomer model)
        {
            ResponseDTO? response = await _convertService.Convert(model);

            if (response == null || !response.IsSuccess)
                return BadRequest(response?.Message);

            return Ok(response);
        }
    }
}