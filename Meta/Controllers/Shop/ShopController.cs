using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class ShopController : Controller
    {
        private string address = "shop";
        private string token = "test_token_pjewggor32c7634g0669c1j8e701je10";
        private Meta meta;

        public override void Init(Meta meta)
        {
            this.meta = meta;
            MetaEvents.OnWebResponse += OnWebResponse;
            MetaEvents.OnGetRequest?.Invoke(address);
        }

        private void OnWebResponse(string address, string json)
        {
            if (this.address == address)
            {
                meta.data.shop = JsonUtility.FromJson<Shop>(json);
            }
        }

        private void BannerClick()
        {
            
        }

    }
}