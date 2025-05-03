using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Genre;

public class GenreCreateDto
{
    [Required]
    public string Name { get; set; }
}