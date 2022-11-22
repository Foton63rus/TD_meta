using System.Collections;
using TowerDefence;
using UnityEngine;
using UnityEngine.Networking;

public class JsonController : Controller
{
    private string jsonUrl;
    private string response;
    public override void Init(Meta meta)
    {
        MetaEvents.OnGetRequest += OnGetRequest;
        jsonUrl = meta.data.jsonUrl;
    }

    public IEnumerator GetRequest(string urlPostFix)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get( jsonUrl+urlPostFix))
        {
            yield return webRequest.SendWebRequest();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log("Received: " + webRequest.downloadHandler.text);
                    response = webRequest.downloadHandler.text;
                    MetaEvents.OnServerJsonResponse?.Invoke(urlPostFix, response);
                    MetaEvents.OnWebResponse?.Invoke(urlPostFix, response);
                    break;
            }
        }
    }
    public void OnGetRequest(string urlPostFix)
    {
        StartCoroutine(GetRequest(urlPostFix));
    }
}
