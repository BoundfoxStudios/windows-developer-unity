using System;
using UnityEngine;

namespace BoundfoxStudios.Computermuseum
{
  public class DoorInteraction : MonoBehaviour, IInteractable
  {
    public Animator Animator;

    private bool _isOpen;
    
    public void Interact()
    {
      _isOpen = !_isOpen;
      
      Animator.SetBool("IsOpen", _isOpen);
    }
  }
}
