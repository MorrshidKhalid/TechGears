namespace  TechGears.Services.CustomerAPI.Models.DTO
{
    public class LeadToCustomer
    {
        public int LeadId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Indestry { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = null!;
    }
}