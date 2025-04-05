namespace TechGears.Services.LeadManagmentAPI.Models
{
    public class Lead
    {
        public int LeadId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Indestry { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public required Source Source { get; set; }
        public required Status Status { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public string AssignedTo { get; set; } = null!;
    }
}
