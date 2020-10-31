using System;
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

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Manga>().HasKey(m => m.Id);

            modelBuilder.Entity<Chapter>().HasKey(c => c.Id);

            modelBuilder.Entity<Genre>().HasKey(g => g.Id);
            
            modelBuilder.Entity<Profile>().HasKey(p => p.Id);

            modelBuilder.Entity<Staff>().HasKey(s => s.Id);

            modelBuilder.Entity<Theme>().HasKey(t => t.Id);
        }
    }
}
