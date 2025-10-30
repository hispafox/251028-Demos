namespace TodoApp.Api.DTOs
{
    /// <summary>
    /// DTO para lectura de tareas
    /// </summary>
    public class TodoItemDto
    {
     public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
