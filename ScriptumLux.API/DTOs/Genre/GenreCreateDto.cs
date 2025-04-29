using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.Genre;

public class GenreCreateDto
{
    [Required]
    public string Name { get; set; }
}