using System;
using System.Collections;
using UnityEngine;

namespace BoundfoxStudios.Computermuseum
{
  public class MuseumObjectRotator : MonoBehaviour
  {
    public GameObject MuseumObject;
    public MonoBehaviour[] AdditionalControls;
    public float TimeToLift = 1;
    public float LiftAmount = 0.25f;
    public float Sensitivity = 100;
    public Collider Collider;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private bool _processControls;
    private PlayerController _playerController;
    private Vector3 _center;
    
    private void Awake()
    {
      _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
      _center = Collider.bounds.center;
    }

    private void Update()
    {
      if (!_processControls)
      {
        return;
      }

      RotateObject();

      if (Input.GetKeyDown(KeyCode.F))
      {
        _processControls = false;
        
        foreach (var additionalControl in AdditionalControls)
        {
          additionalControl.enabled = false;
        }

        StartCoroutine(Lift(LiftAmount, () =>
        {
          MuseumObject.transform.position = _initialPosition;
          MuseumObject.transform.rotation = _initialRotation;
          enabled = false;
          _playerController.enabled = true;
        }));
      }
    }

    private void OnEnable()
    {
      _playerController.enabled = false;
      
      _initialPosition = MuseumObject.transform.position;
      _initialRotation = MuseumObject.transform.rotation;

      StartCoroutine(Lift(-LiftAmount, () =>
      {
        _processControls = true;
        
        foreach (var additionalControl in AdditionalControls)
        {
          additionalControl.enabled = true;
        }
      }));
    }

    private void RotateObject()
    {
      var roll = Input.GetAxis("Roll") * Sensitivity * Time.deltaTime;
      var pitch = Input.GetAxis("Vertical") * Sensitivity * Time.deltaTime;
      var yaw = Input.GetAxis("Horizontal") * Sensitivity * Time.deltaTime;
      
      MuseumObject.transform.RotateAround(_center, Vector3.up, roll);
      MuseumObject.transform.RotateAround(_center, Vector3.forward, yaw);
      MuseumObject.transform.RotateAround(_center, Vector3.right, pitch);
    }

    private IEnumerator Lift(float amount, Action done)
    {
      var t = 0f;
      var startY = MuseumObject.transform.position.y;
      var endY = startY - amount;
      var startRotation = MuseumObject.transform.rotation;
      var endRotation = _initialRotation;

      while (t < 1)
      {
        var calculatedY = Mathf.Lerp(startY, endY, t / TimeToLift);
        var calculatedRotation = Quaternion.Lerp(startRotation, endRotation, t / TimeToLift);

        MuseumObject.transform.position = new Vector3(
          MuseumObject.transform.position.x,
          calculatedY,
          MuseumObject.transform.position.z
        );

        MuseumObject.transform.rotation = calculatedRotation;

        t += Time.deltaTime;

        yield return null;
      }

      done?.Invoke();
    }
  }
}
