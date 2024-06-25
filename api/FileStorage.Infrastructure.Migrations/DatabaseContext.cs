using FileStorage.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Migrations
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>(builder =>
            {
                builder.HasKey(e => e.Id);
                builder.Property(e => e.Name).HasMaxLength(200);
                builder.HasIndex(e => e.Name).IsUnique();
            });
        }
    }
}
