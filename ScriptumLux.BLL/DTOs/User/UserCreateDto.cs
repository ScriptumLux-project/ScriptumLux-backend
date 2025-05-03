using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.User;

public class UserCreateDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }  // Пароль буде хешуватись на сервері

    public string Role { get; set; } = "User";  // Значення за замовчуванням

}