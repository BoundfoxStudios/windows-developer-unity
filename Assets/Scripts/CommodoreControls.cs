using System;
using UnityEngine;

namespace BoundfoxStudios.Computermuseum
{
  public class CommodoreControls : MonoBehaviour
  {
    public Animator Animator;

    private bool _isOpen;

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.O))
      {
        _isOpen = !_isOpen;
        Animator.SetBool("IsOpen", _isOpen);
      }
    }
  }
}
