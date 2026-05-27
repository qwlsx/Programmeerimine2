using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    // 28.11 Pärib Entity klassist
    public class ToDoList : Entity
    {

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public IList<ToDoItem> Items { get; set; }

        // 15.11.2025
        // Väärtuste Items automaatselt ära (väldime mujal NullReferenceException)
        public ToDoList()
        {
            Items = new List<ToDoItem>();
        }
    }
}
