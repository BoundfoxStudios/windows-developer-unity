using System;
using BoundfoxStudios.Computermuseum.DTOs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BoundfoxStudios.Computermuseum
{
  public class MuseumInformationUi : UIBehaviour
  {
    public GameObject ButtonContainer;
    public GameObject TabContainer;
    public GameObject ButtonPrefab;
    public GameObject TabPrefab;
    public TextMeshProUGUI HeaderText;
    public TextMeshProUGUI DescriptionText;
    public RawImage Image;

    private readonly Lazy<MuseumObjectManager> _museumObjectManager = new Lazy<MuseumObjectManager>(FindObjectOfType<MuseumObjectManager>);
    private readonly KeyCode[] keysToBind = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };

    public void SetInformation(MuseumInformation information)
    {
      ClearContainer(ButtonContainer);
      ClearContainer(TabContainer);

      HeaderText.text = information.Name;
      DescriptionText.text = information.Description;

      _museumObjectManager.Value.GetImage(information.IdName, texture => Image.texture = texture, _ => Debug.LogError("Can not get image..."));

      for (var index = 0; index < information.InformationPages.Length; index++)
      {
        AddPage(information.InformationPages[index], index);
      }

      if (TabContainer.transform.childCount > 0)
      {
        TabContainer.transform.GetChild(0).gameObject.SetActive(true);
      }
    }

    public void SetError(string error)
    {
      HeaderText.text = "Error getting info";
      DescriptionText.text = error;
    }

    private void Update()
    {
      for (var i = 0; i < keysToBind.Length; i++)
      {
        if (Input.GetKeyDown(keysToBind[i]))
        {
          SetPage(i); 
        }
      }
    }

    private void SetPage(int index)
    {
      foreach (Transform t in ButtonContainer.transform)
      {
        var image = t.GetComponent<Image>();
        image.color = Color.white;
      }
      
      foreach (Transform t in TabContainer.transform)
      {
        t.gameObject.SetActive(false);
      }
      
      var buttonImage = ButtonContainer.transform.GetChild(index).GetComponent<Image>();
      buttonImage.color = Color.gray;
      
      TabContainer.transform.GetChild(index).gameObject.SetActive(true);
    }

    private void ClearContainer(GameObject container)
    {
      for (var i = container.transform.childCount - 1; i >= 0; i--)
      {
        var child = container.transform.GetChild(i);
        Destroy(child.gameObject);
      }
    }

    private void AddPage(InformationPage page, int index)
    {
      var button = Instantiate(ButtonPrefab, ButtonContainer.transform);
      var tab = Instantiate(TabPrefab, TabContainer.transform);

      var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
      buttonText.text = $"({index + 1}) {page.Title}";

      var tabText = tab.GetComponent<TextMeshProUGUI>();
      tabText.text = page.Description;
      
      tab.SetActive(false);
    }
  }
}
