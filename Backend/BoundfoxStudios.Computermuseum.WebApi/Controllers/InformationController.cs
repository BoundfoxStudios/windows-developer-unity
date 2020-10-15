using System.Threading.Tasks;
using BoundfoxStudios.Computermuseum.WebApi.Data.Models;
using BoundfoxStudios.Computermuseum.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoundfoxStudios.Computermuseum.WebApi.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class InformationController : ControllerBase
  {
    private readonly InformationService _informationService;

    public InformationController(InformationService informationService)
    {
      _informationService = informationService;
    }
    
    [HttpGet("{idName}")]
    public async Task<ActionResult<MuseumItem>> GetInformationAsync(string idName)
    {
      return Ok(await _informationService.GetInformationAsync(idName));
    }
  }
}
