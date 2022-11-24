using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace TowerDefence
{
    public interface IWebController
    {
        public void Get(string address);
        public void Get(string address, string[] args );
    }
    
    public class WebController : Controller, IWebController
    {
        private string url;
        private string token;
        private string response;

        public override void Init( Meta meta )
        {
            url = meta.data.url;
            token = meta.data.token;
            MetaEvents.WebGetRequest += Get;
            MetaEvents.WebGetRequestWithArgs += Get;
        }
        
        public WebController Initialize( Meta meta )
        {
            Init(meta);
            return this;
        }

        public IEnumerator GetRequest( URL requestedURL )
        {
            Debug.Log($"url:{requestedURL}");
            using (UnityWebRequest webRequest = UnityWebRequest.Get( requestedURL.Get() ))
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
                        response = webRequest.downloadHandler.text;
                        MetaEvents.OnServerJsonResponse?.Invoke(requestedURL.GetLocalAddress(), response);
                        MetaEvents.OnWebResponse?.Invoke(requestedURL.GetLocalAddress(), response);
                        break;
                }
            }
        }
        public void Get(string urlPostFix)
        {
            Get(urlPostFix, null);
        }
        public void Get(string urlPostFix, string[] args = null)
        {
            StartCoroutine(GetRequest( 
                new URL( url, urlPostFix, token, args )));
        }
    }

    public struct URL
    {
        private string address;
        private string requestedUrl;

        public URL(string url, string address, string token, string[] args)
        {
            this.address = address;
            string strArgs = "";

            if (args != null && args.Length > 0)
            {
                strArgs = $"?token={token}";
                for (int i = 0; i < args.Length; i++)
                {
                    strArgs += $",{args[i]}";
                }
            }
            requestedUrl = $"{url}{address}{strArgs}";
        }

        public string Get()
        {
            return requestedUrl;
        }

        public string GetLocalAddress()
        {
            return address;
        }

        public override string ToString()
        {
            return requestedUrl;
        }
    }
}