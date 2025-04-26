namespace ScriptumLux.DAL.Entities;

public class History
{
    // Composite key configured in DbContext
    public int UserId { get; set; }
    public User User { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public DateTime ViewedAt { get; set; }
}
