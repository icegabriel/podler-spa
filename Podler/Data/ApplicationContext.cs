using Microsoft.EntityFrameworkCore;
using Podler.Models;
using Podler.Models.Mangas;

namespace Podler.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Chapter> Chapers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<ImagePage> ImagePages { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Manga>().HasKey(m => m.Id);
            modelBuilder.Entity<Manga>().HasMany(m => m.Chapters).WithOne(c => c.Manga);

            modelBuilder.Entity<Chapter>().HasKey(c => c.Id);
            modelBuilder.Entity<Chapter>().HasOne(c => c.Manga);
            modelBuilder.Entity<Chapter>().HasMany(c => c.Pages).WithOne(ip => ip.Chapter);

            modelBuilder.Entity<MangaGenre>().HasKey(mg => new { mg.MangaId, mg.GenreId });
            modelBuilder.Entity<MangaGenre>().HasOne(mg => mg.Manga)
                                             .WithMany(m => m.Genres)
                                             .HasForeignKey(mg => mg.MangaId);

            modelBuilder.Entity<MangaStaff>().HasKey(ms => new { ms.MangaId, ms.StaffId });
            modelBuilder.Entity<MangaStaff>().HasOne(ms => ms.Manga)
                                             .WithMany(m => m.Staffs)
                                             .HasForeignKey(ms => ms.MangaId);

            modelBuilder.Entity<MangaTheme>().HasKey(mt => new { mt.MangaId, mt.ThemeId });
            modelBuilder.Entity<MangaTheme>().HasOne(mt => mt.Manga)
                                             .WithMany(m => m.Themes)
                                             .HasForeignKey(mt => mt.MangaId);

            modelBuilder.Entity<ImagePage>().HasKey(ip => ip.Id);
            modelBuilder.Entity<ImagePage>().HasOne(ip => ip.Chapter);

            modelBuilder.Entity<Profile>().HasKey(p => p.Id);
            modelBuilder.Entity<Profile>().HasMany(p => p.Favorites);
        }
    }
}
