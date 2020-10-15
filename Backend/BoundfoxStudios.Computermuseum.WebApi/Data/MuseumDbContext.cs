using BoundfoxStudios.Computermuseum.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoundfoxStudios.Computermuseum.WebApi.Data
{
  public class MuseumDbContext : DbContext
  {
    public DbSet<MuseumItem> MuseumItems { get; set; } = default!;
    
    public MuseumDbContext(DbContextOptions<MuseumDbContext> options)
      : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder
        .Entity<MuseumItem>()
        .HasKey(item => item.Id);

      modelBuilder
        .Entity<MuseumItem>()
        .HasAlternateKey(item => item.IdName);

      modelBuilder
        .Entity<MuseumItem>()
        .HasMany(item => item.InformationPages);
    }
  }
}
