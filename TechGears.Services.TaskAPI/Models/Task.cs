namespace TechGears.Services.TaskAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public TaskType TaskType { get; set; } = TaskType.ToDo;
        public Priority Priority { get; set; }
        public int AssignedTo { get; set; } = -1;
        public DateTime DueDate { get; set; }
    }
}
