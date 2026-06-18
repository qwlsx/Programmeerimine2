namespace KooliProjekt.Application.DTOs
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}