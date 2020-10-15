using System.Threading.Tasks;
using BoundfoxStudios.Computermuseum.WebApi.Data;
using BoundfoxStudios.Computermuseum.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoundfoxStudios.Computermuseum.WebApi.Services
{
  public class InformationService
  {
    private readonly MuseumDbContext _context;

    public InformationService(MuseumDbContext context)
    {
      _context = context;
    }

    public async Task<MuseumItem> GetInformationAsync(string idName)
    {
      return await _context.MuseumItems
        .Include(item => item.InformationPages)
        .SingleOrDefaultAsync(item => item.IdName == idName);
    }
  }
}
