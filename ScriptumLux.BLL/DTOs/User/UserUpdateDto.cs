using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.User;

public class UserUpdateDto
{
    [Required]
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string? Password { get; set; }  // Якщо потрібно змінити пароль
}