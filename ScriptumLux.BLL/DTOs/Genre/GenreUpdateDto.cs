using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Genre;

public class GenreUpdateDto
{
    [Required]
    public string Name { get; set; }
}