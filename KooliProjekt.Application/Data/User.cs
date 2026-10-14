using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    // Id от Entity
    public class User : Entity
    {
        [Required(ErrorMessage = "Имя пользователя обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Имя пользователя не может быть длиннее 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email адреса")]
        [StringLength(100, ErrorMessage = "Email не может быть длиннее 100 символов")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Роль пользователя обязательна")]
        [StringLength(20, ErrorMessage = "Роль не может быть длиннее 20 символов")]
        public string Role { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
