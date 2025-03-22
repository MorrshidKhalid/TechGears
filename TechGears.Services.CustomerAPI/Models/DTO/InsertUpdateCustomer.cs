namespace TechGears.Services.CustomerAPI.Models.DTO
{
    public class InsertUpdateCustomer
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Indestry { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public CustomerType Type { get; set; } = CustomerType.C;
        public CustomerStatus Status { get; set; } = CustomerStatus.Avaliable;
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public int LeadId { get; set; } = -1;
        public int AssignedTo { get; set; } = -1;
    }
}
