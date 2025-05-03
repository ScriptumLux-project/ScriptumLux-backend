namespace ScriptumLux.BLL.DTOs.User;

public class UserDto
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool IsBanned { get; set; }
}