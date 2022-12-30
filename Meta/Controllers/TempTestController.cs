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
            MetaEvents.WebGetRequest?.Invoke($"mirror");
            MetaEvents.OnWebResponse += OnWebResponse;

            TestFunction();
        }

        private void OnWebResponse(string addr, string resp)
        {
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //var obj = Activator.CreateInstance(Type.GetType("TowerDefence.Coin"));
                //Debug.Log($"type:{obj.GetType()}");
            }
        }

        public void TestFunction()
        {
            
        }
    }
}
