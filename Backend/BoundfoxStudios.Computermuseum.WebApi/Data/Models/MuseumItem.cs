using System.Collections.Generic;

namespace BoundfoxStudios.Computermuseum.WebApi.Data.Models
{
  /// <summary>
  /// Represents an item in the museum, e.g. the C64.
  /// </summary>
  public class MuseumItem
  {
    public int Id { get; set; }
    public string IdName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;

    public ICollection<InformationPage> InformationPages { get; set; } = new List<InformationPage>();
  }
}
