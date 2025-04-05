namespace  TechGears.Services.LeadManagmentAPI.Models.DTO
{
    public class LeadToCustomer
    {
        public int LeadId { get; set; }
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? CompanyName { get; set; } = null!;
        public string? Indestry { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public string? AssignedTo { get; set; } = null!;
    }
}