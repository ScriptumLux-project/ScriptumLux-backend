namespace ScriptumLux.DAL.Entities;

public class PlaylistMovie
{
    // Composite key configured in DbContext
    public int PlaylistId { get; set; }
    public Playlist Playlist { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }
}