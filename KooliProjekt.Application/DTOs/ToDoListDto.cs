using System.Collections.Generic;

namespace KooliProjekt.Application.DTOs
{
    public class ToDoListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<ToDoItemDto> Items { get; set; } = new();
    }
}