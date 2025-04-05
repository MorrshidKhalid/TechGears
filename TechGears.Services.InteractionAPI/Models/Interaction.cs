namespace TechGears.Services.InteractionAPI.Models.Interaction
{
    public class Interaction
    {
        public int InteractionId { get; set; }
        public int? CustomerId { get; set; } = null!;
        public int? UserId { get; set; } = null!;
        public InteractionType Type {get; set; }
        public string Note {get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}