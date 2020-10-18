using UnityEngine;

namespace BoundfoxStudios.Computermuseum
{
  public class MuseumObject : MonoBehaviour, IInteractable
  {
    public string Id;
    public GameObject InformationCanvas;
    public MuseumInformationUi InformationUi;
    public MuseumObjectRotator MuseumObjectRotator;

    private MuseumObjectManager _manager;
    private bool _processControls;

    private void Awake()
    {
      _manager = FindObjectOfType<MuseumObjectManager>();
    }

    private void Start()
    {
      _manager.GetMuseumInformation(Id, information => InformationUi.SetInformation(information), error => InformationUi.SetError(error));
    }

    private void OnEnable()
    {
      _manager.Register(this);
    }

    private void OnDisable()
    {
      _manager.Unregister(this);
    }

    public void Interact()
    {
      _processControls = true;
      MuseumObjectRotator.enabled = true;
      InformationCanvas.gameObject.SetActive(true);
      InformationUi.gameObject.SetActive(false);
    }

    private void Update()
    {
      if (!_processControls)
      {
        return;
      }

      if (Input.GetKeyDown(KeyCode.F))
      {
        _processControls = false;
        InformationCanvas.gameObject.SetActive(false);
        MuseumObjectRotator.Settle(() => { MuseumObjectRotator.enabled = false; });
      }

      if (Input.GetKeyUp(KeyCode.I))
      {
        InformationUi.gameObject.SetActive(!InformationUi.gameObject.activeSelf);
      }
    }
  }
}
