using UnityEngine;

namespace BoundfoxStudios.Computermuseum
{
  public class PlayerController : MonoBehaviour
  {
    public float Acceleration = 5f;
    public Transform InteractableRaycastStart;
    public float InteractableRaycastLength = 0.75f;
    public MouseLook MouseLook;
    
    private Vector2 _movement = Vector2.zero;
    private Rigidbody _rigidbody;

    private void Start()
    {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
      MouseLook.enabled = true;
    }

    private void OnDisable()
    {
      MouseLook.enabled = false;
    }

    private void Update()
    {
      // var 
      var turn = Input.GetAxis("Horizontal");
      var acceleration = Input.GetAxis("Vertical");
      
      _movement = new Vector2(turn, acceleration);

      InteractWithDoor();
    }

    private void InteractWithDoor()
    {
      if (Physics.Raycast(InteractableRaycastStart.position, transform.forward , out var hit, 0.75f, ~LayerMask.NameToLayer("Interactables")))
      {
        if (hit.collider.CompareTag("Interactable") && Input.GetKeyDown(KeyCode.F))
        {
          var interactable = hit.collider.GetComponent<IInteractable>();
          interactable?.Interact();
        }
      }
    }

    private void FixedUpdate()
    {
      var deltaMovement = _rigidbody.transform.right * _movement.x+  _rigidbody.transform.forward * _movement.y;
      _rigidbody.MovePosition(_rigidbody.position + deltaMovement * Acceleration);
    }

    private void OnDrawGizmosSelected()
    {
      if (!InteractableRaycastStart)
      {
        return;
      }
      
      Gizmos.color = Color.red;
      Gizmos.DrawRay(InteractableRaycastStart.position, transform.forward * InteractableRaycastLength);
    }
  }
}
