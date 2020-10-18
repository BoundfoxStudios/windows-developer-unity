using UnityEngine;

namespace BoundfoxStudios.Computermuseum
{
  public class MuseumObjectInteraction : MonoBehaviour, IInteractable
  {
    public MuseumObjectRotator MuseumObjectRotator;
    
    public void Interact()
    {
      MuseumObjectRotator.enabled = true;
    }
  }
}
