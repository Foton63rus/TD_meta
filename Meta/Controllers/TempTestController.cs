using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class TempTestController : Controller
    {
        private Meta meta;
        public override void Init(Meta meta)
        {
            this.meta = meta;
            MetaEvents.WebGetRequest?.Invoke($"mirror?id={123354}");
            
            // MetaEvents.WebGetRequest += WebRequest;
            // MetaEvents.WebGetRequestWithArgs += WebRequestWithArgs;
            MetaEvents.OnWebResponse += OnWebResponse;
        }

        private void WebRequest(string addr)
        {
            meta.Web.Get(addr);
        }
        private void WebRequestWithArgs(string addr, string[] args)
        {
            meta.Web.Get(addr, args);
        }

        private void OnWebResponse(string addr, string resp)
        {
            // Debug.Log(resp);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //var obj = Activator.CreateInstance(Type.GetType("TowerDefence.Coin"));
                //Debug.Log($"type:{obj.GetType()}");
            }
        }
    }
}
