using Microsoft.EntityFrameworkCore;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.DAL;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistMovie> PlaylistMovies { get; set; }
        public DbSet<History> HistoryRecords { get; set; }
        public DbSet<Timecode> Timecodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite PKs
            modelBuilder.Entity<PlaylistMovie>()
                .HasKey(pm => new { pm.PlaylistId, pm.MovieId });

            modelBuilder.Entity<History>()
                .HasKey(h => new { h.UserId, h.MovieId, h.ViewedAt });

            // Relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Playlists)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.HistoryRecords)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Timecodes)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Comments)
                .WithOne(c => c.Movie)
                .HasForeignKey(c => c.MovieId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Reviews)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.PlaylistMovies)
                .WithOne(pm => pm.Movie)
                .HasForeignKey(pm => pm.MovieId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.HistoryRecords)
                .WithOne(h => h.Movie)
                .HasForeignKey(h => h.MovieId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Timecodes)
                .WithOne(t => t.Movie)
                .HasForeignKey(t => t.MovieId);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId);

            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.PlaylistMovies)
                .WithOne(pm => pm.Playlist)
                .HasForeignKey(pm => pm.PlaylistId);
        }
    }