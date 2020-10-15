using Microsoft.EntityFrameworkCore;

namespace BoundfoxStudios.Computermuseum.WebApi.Data
{
  public class MuseumDbContext : DbContext
  {
    public MuseumDbContext(DbContextOptions<MuseumDbContext> options)
      : base(options) { }
  }
}
