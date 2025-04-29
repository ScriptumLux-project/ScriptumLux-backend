using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.Genre;

public class GenreUpdateDto
{
    [Required]
    public string Name { get; set; }
}