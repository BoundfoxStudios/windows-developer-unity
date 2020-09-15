using UnityEngine;

namespace BoundfoxStudios.Computermuseum
{
  public class MouseLook : MonoBehaviour
  {
    public float Sensitivity = 100;
    public Transform Body;
    public float MinYLook = -90;
    public float MaxYLook = 90;

    private float _xRotation;

    private void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
      var mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
      var mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

      _xRotation -= mouseY;
      _xRotation = Mathf.Clamp(_xRotation, MinYLook, MaxYLook);

      transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
      Body.Rotate(Vector3.up * mouseX);
    }
  }
}
