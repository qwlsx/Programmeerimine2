using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    // 28.11
    // Pärib Entity klassist
    public class ToDoItem : Entity
    {

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public bool IsDone { get; set; }

        public ToDoList ToDoList { get; set; }
        public int ToDoListId { get; set; }
    }
}