using System;
using System.Collections;
using BoundfoxStudios.Computermuseum.DTOs;
using UnityEngine;
using UnityEngine.Networking;

namespace BoundfoxStudios.Computermuseum
{
  public class MuseumInformationLoader : MonoBehaviour
  {
#if !DEBUG
    private string _apiUrl = "https://computermuseum-api.herokuapp.com";
#else
    private string _apiUrl = "https://localhost:5001";
#endif

    public void GetInformation(string id, Action<MuseumInformation> done, Action<string> error)
    {
      var webRequest = UnityWebRequest.Get($"{_apiUrl}/api/information/{id}");

      StartCoroutine(MakeApiRequest(webRequest, done, error));
    }

    public void GetImage(string url, Action<Texture> done, Action<string> error)
    {
      var webRequest = UnityWebRequestTexture.GetTexture(url);

      StartCoroutine(MakeTextureRequest(webRequest, done, error));
    }

    private IEnumerator MakeApiRequest<T>(UnityWebRequest request, Action<T> done, Action<string> error)
    {
      Debug.LogFormat("Sending web request... {0}", request.url);

      yield return request.SendWebRequest();

      var isError = request.isNetworkError || request.isHttpError;

      Debug.LogFormat("Got answer, is error {0}", isError);

      if (isError)
      {
        error(request.error);
        yield break;
      }
      
      
      var deserializedObject = JsonUtility.FromJson<T>(request.downloadHandler.text);
      done(deserializedObject);
    }
    
    private IEnumerator MakeTextureRequest(UnityWebRequest request, Action<Texture2D> done, Action<string> error)
    {
      Debug.Log("Sending texture web request...");

      yield return request.SendWebRequest();

      var isError = request.isNetworkError || request.isHttpError;

      Debug.LogFormat("Got texture answer, is error {0}", isError);

      if (isError)
      {
        error(request.error);
        yield break;
      }
      
      done(DownloadHandlerTexture.GetContent(request));
    }
  }
}
