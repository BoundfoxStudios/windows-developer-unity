using System;
using System.Collections.Generic;

namespace BoundfoxStudios.Computermuseum.DTOs
{
  [Serializable]
  public class MuseumInformation
  {
    public string IdName;
    public string Name;
    public string Description;
    public string ImageUrl;

    public InformationPage[] InformationPages;
  }
}
