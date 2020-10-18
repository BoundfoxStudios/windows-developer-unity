using System;
using System.Collections.Generic;
using BoundfoxStudios.Computermuseum.DTOs;
using UnityEngine;

namespace BoundfoxStudios.Computermuseum
{
  public class MuseumObjectManager : MonoBehaviour
  {
    private readonly Dictionary<string, MuseumObject> _museumObjects = new Dictionary<string, MuseumObject>();
    private readonly Dictionary<string, MuseumInformation> _museumObjectInformation = new Dictionary<string, MuseumInformation>();
    private readonly Dictionary<string, Texture> _museumObjectImages = new Dictionary<string, Texture>();
    
    private MuseumInformationLoader _museumInformationLoader;

    private void Awake()
    {
      _museumInformationLoader = FindObjectOfType<MuseumInformationLoader>();
    }

    public void Unregister(MuseumObject museumObject)
    {
      _museumObjects.Remove(museumObject.Id);
    }

    public void Register(MuseumObject museumObject)
    {
      _museumObjects.Add(museumObject.Id, museumObject);
    }

    public void GetMuseumInformation(string id, Action<MuseumInformation> done, Action<string> error)
    {
      if (_museumObjectInformation.ContainsKey(id))
      {
        done(_museumObjectInformation[id]);
        return;
      }
      
      _museumInformationLoader.GetInformation(id, information =>
      {
        _museumObjectInformation.Add(id, information);
        done(information);
      }, error);
    }

    public void GetImage(string id, Action<Texture> done, Action<string> error)
    {
      if (_museumObjectImages.ContainsKey(id))
      {
        done(_museumObjectImages[id]);
        return;
      }

      var museumInformation = _museumObjectInformation[id];
      _museumInformationLoader.GetImage(museumInformation.ImageUrl, texture =>
      {
        _museumObjectImages.Add(id, texture);
        done(texture);
      }, error);
    }
  }
}
