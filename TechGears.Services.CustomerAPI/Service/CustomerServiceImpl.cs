using Microsoft.EntityFrameworkCore;
using TechGears.Services.CustomerAPI.Data;
using TechGears.Services.CustomerAPI.Models;
using TechGears.Services.CustomerAPI.Models.DTO;
using TechGears.Services.CustomerAPI.Service.IService;

namespace TechGears.Services.CustomerAPI.Service
{
    // Handle simple CRUD.
    public class CustomerServiceImpl : ICustomerService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ResponseDTO _responseDTO;

        public CustomerServiceImpl(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _responseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO?> CreateAsync(InsertUpdateCustomer insert)
        {
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

        public async Task<ResponseDTO?> DeleteAsync(int customrtId)
        {
            if (customrtId <= 0)
                return ErrorResponse(msg: $"Invalid id ({customrtId})");

            Customer? selectedCustomer = await Find(customrtId);
            if (selectedCustomer != null)
            {
                _appDbContext.Remove(selectedCustomer);
                await _appDbContext.SaveChangesAsync();
                return Success(null, msg: $"Customer with id ({customrtId} deleted)");
            }

            return ErrorResponse(msg: $"Customer with id ({customrtId} is not exists)");
        }

        public async Task<ResponseDTO?> GetAllCustomersAsync()
        {
            if (!_appDbContext.Customers.Any())
                return ErrorResponse(msg: "No customers to show");

            var result = await _appDbContext.Customers.Select(customer => CustomerToDTO(customer)).ToListAsync();

            return Success(result);
        }

        public async Task<ResponseDTO?> GetCustomerByIdAsync(int customrtId)
        {
            if (customrtId <= 0)
                return ErrorResponse(msg: $"Invalid id ({customrtId})");

            var selectedCustomer = await Find(customrtId);

            if (selectedCustomer == null)
                 return ErrorResponse(msg: $"Customer with id ({customrtId}) not found");

            return Success(
                CustomerToDTO(selectedCustomer), 
                msg:(selectedCustomer != null) ? "Found." : $"Customer with id ({customrtId}) not found.");
        }

        public async Task<ResponseDTO?> UpdateAsync(int customrtId, InsertUpdateCustomer update)
        {
            if (customrtId <= 0)
                return ErrorResponse(msg: $"Invalid id ({customrtId})");

            Customer? selectedCustomer = await _appDbContext.Customers.SingleAsync(c => c.CustomerId == customrtId);

            if (selectedCustomer != null)
            {
                selectedCustomer.FirstName = update.FirstName;
                selectedCustomer.LastName = update.LastName;
                selectedCustomer.CompanyName = update.CompanyName;
                selectedCustomer.Email = update.Email;
                selectedCustomer.Phone = update.Phone;
                selectedCustomer.Indestry = update.Indestry;
                selectedCustomer.Address = update.Address;
                selectedCustomer.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

                await _appDbContext.SaveChangesAsync();
                return Success(CustomerToDTO(selectedCustomer), msg: "Updated");
            }

            return ErrorResponse(msg: $"customer with id ({customrtId}) not found");
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

        private async Task<Customer?> Find(int customerId) => await _appDbContext.Customers.SingleOrDefaultAsync(customer => customer.CustomerId == customerId);
        
        private CustomerDTO CustomerToDTO(Customer c) => new()
        {
            FirstName = c.FirstName,
            LastName = c.LastName,
            CompanyName = c.CompanyName,
            Address = c.Address,
            Indestry = c.Indestry,
            Email = c.Email,
            Phone = c.Phone
        };
        
    }
}