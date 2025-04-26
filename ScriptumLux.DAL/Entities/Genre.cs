using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.DAL.Entities;

public class Genre
{
    [Key]
    public int GenreId { get; set; }

    [Required]
    public string Name { get; set; }

    // Navigation
    public ICollection<Movie> Movies { get; set; }
}