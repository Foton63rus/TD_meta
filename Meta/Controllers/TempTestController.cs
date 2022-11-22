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

            MetaEvents.OnGetRequest.Invoke($"mirror?id={123354}");
            MetaEvents.OnServerJsonResponse += OnServerJsonResponse;
        }

        private void OnServerJsonResponse(string addr, string resp)
        {
            Debug.Log(resp);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //var obj = Activator.CreateInstance(Type.GetType("TowerDefence.Coin"));
                //Debug.Log($"type:{obj.GetType()}");
                
                Product product = new Product( );
                product.cards = new[] {new PlayerCard(1, 1, 0)};
                Debug.Log(JsonUtility.ToJson(product));
            }
        }
        public interface IInterface
        {
            public int getAmount();
        }
        [Serializable]
        public class MyClass : IInterface
        {
            public int amount = 5;
            public int getAmount()
            {
                return amount;
            }
        }
    }
}
