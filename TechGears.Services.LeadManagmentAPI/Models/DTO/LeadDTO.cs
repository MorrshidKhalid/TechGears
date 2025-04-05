namespace TechGears.Services.LeadManagmentAPI.Models.DTO
{
    public class LeadDTO
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
    }
}
