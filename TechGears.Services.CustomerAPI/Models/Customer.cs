namespace TechGears.Services.CustomerAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string Indestry { get; set; } = null!;
        public required string Email { get; set; }
        public string Phone { get; set; } = null!;
        public CustomerType Type { get; set; }
        public CustomerStatus Status { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public int? LeadId { get; set; } = null!;
        public string? AssignedTo { get; set; } = null!;
    }
}
