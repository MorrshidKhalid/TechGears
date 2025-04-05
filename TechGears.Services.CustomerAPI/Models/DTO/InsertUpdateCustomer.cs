namespace TechGears.Services.CustomerAPI.Models.DTO
{
    public class InsertUpdateCustomer
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string Indestry { get; set; } = string.Empty;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public CustomerType Type { get; set; } = CustomerType.C;
        public CustomerStatus Status { get; set; } = CustomerStatus.Avaliable;
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public int LeadId { get; set; } = -1;
        public string? AssignedTo { get; set; } = null!;
    }
}
