using System.Collections.Generic;
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

    /// <summary>
    /// Returns a list of the available museums items.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetAvailableMuseumItemsAsync()
    {
      return Ok(await _informationService.GetAvailableMuseumItemsAsync());
    }
    
    /// <summary>
    /// Return specific information for a single museum item.
    /// </summary>
    /// <param name="idName">The id of the museum item.</param>
    [HttpGet("{idName}")]
    public async Task<ActionResult<MuseumItem>> GetInformationAsync(string idName)
    {
      return Ok(await _informationService.GetInformationAsync(idName));
    }
  }
}
