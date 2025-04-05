namespace TechGears.Services.LeadManagmentAPI.Models.DTO
{
    public class InsertUpdateLead
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Indestry { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Source Source { get; set; } = Source.Call;
        public Status Status { get; set; } = Status.New;
        public string? AssignedTo { get; set; } = null!;
    }
}
