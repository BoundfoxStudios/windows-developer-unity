namespace BoundfoxStudios.Computermuseum.WebApi.Data.Models
{
  /// <summary>
  /// Describes an information page for a museum item, so the information can be split across multiple pages.
  /// </summary>
  public class InformationPage
  {
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
  }
}
