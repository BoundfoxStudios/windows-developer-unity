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
    }

    public void Settle(Action done)
    {
      _processControls = false;

      foreach (var additionalControl in AdditionalControls)
      {
        additionalControl.enabled = false;
      }

      StartCoroutine(Lift(() =>
      {
        MuseumObject.transform.position = _initialPosition;
        MuseumObject.transform.rotation = _initialRotation;
        enabled = false;
        _playerController.enabled = true;
        done?.Invoke();
      }));
    }

    private void OnEnable()
    {
      _playerController.enabled = false;

      _initialPosition = MuseumObject.transform.position;
      _initialRotation = MuseumObject.transform.rotation;

      StartCoroutine(Lift(() =>
      {
        _processControls = true;

        foreach (var additionalControl in AdditionalControls)
        {
          additionalControl.enabled = true;
        }
      }, LiftAmount));
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

    private IEnumerator Lift(Action done, float? amount = null)
    {
      var t = 0f;
      var startPosition = MuseumObject.transform.position;
      var endPosition = _initialPosition;

      if (amount.HasValue)
      {
        endPosition += new Vector3(0, amount.Value, 0);
      }

      var startRotation = MuseumObject.transform.rotation;
      var endRotation = _initialRotation;

      while (t < 1)
      {
        var calculatedPosition = Vector3.Lerp(startPosition, endPosition, t / TimeToLift);
        var calculatedRotation = Quaternion.Lerp(startRotation, endRotation, t / TimeToLift);

        MuseumObject.transform.position = calculatedPosition;
        MuseumObject.transform.rotation = calculatedRotation;

        t += Time.deltaTime;

        yield return null;
      }

      done?.Invoke();
    }
  }
}
