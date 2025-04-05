using TechGears.Services.CustomerAPI.Data;
using TechGears.Services.CustomerAPI.Models;
using TechGears.Services.CustomerAPI.Models.DTO;
using TechGears.Services.CustomerAPI.Service.IService;
using TechGears.Services.CustomerAPI.Utility;

namespace TechGears.Services.CustomerAPI.Service
{
    public class CustomerConvertServiceImpl : TemplateResponse, ICustomerConvertService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserService _userService;

        public CustomerConvertServiceImpl(AppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }

        public async Task<ResponseDTO> Convert(InsertUpdateCustomer insert)
        {
            if (insert.AssignedTo != null && !await _userService.IsUserExists(insert.AssignedTo))
                return ErrorResponse($"User assigned to ({insert.AssignedTo}) not found");

            Customer newCustomer = new()
            {
                FirstName = insert.FirstName,
                LastName = insert.LastName,
                CompanyName = insert.CompanyName,
                Address = insert.Address,
                Indestry = insert.Indestry,
                Email = insert.Email,
                Phone = insert.Phone,
                Status = insert.Status,
                Type = insert.Type,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now),
                LeadId = insert.LeadId,
                AssignedTo = insert.AssignedTo
            };

            await _appDbContext.AddAsync(newCustomer);
            await _appDbContext.SaveChangesAsync();

            if (newCustomer.CustomerId > 0)
                return Success(result: newCustomer);
            else
                return ErrorResponse(msg: "Couldn't create new customer");

        }
    }
}