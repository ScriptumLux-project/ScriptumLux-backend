using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.User;

public class UserCreateDto
{
    [Required] 
    public string Name { get; set; }
    
    [Required, EmailAddress] 
    public string Email { get; set; }
    
    [Required, MinLength(6)] 
    public string Password { get; set; }
    
    [Required, Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
    
    public string Role { get; set; } = "User";
}
