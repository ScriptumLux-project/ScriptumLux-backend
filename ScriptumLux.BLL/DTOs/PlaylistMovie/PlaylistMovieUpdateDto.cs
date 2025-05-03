using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.PlaylistMovie;

public class PlaylistMovieUpdateDto
{
    [Required]
    public int PlaylistId { get; set; }

    [Required]
    public int MovieId { get; set; }

    // Допустим, что мы хотим обновить порядок фильма в плейлисте
    public int? NewOrder { get; set; }  // Пример дополнительного поля для порядка

}