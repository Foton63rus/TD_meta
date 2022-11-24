using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class ShopController : Controller
    {
        private string address = "shop";
        private string addressBuyBanner = @"shop/buy/";
        private Meta meta;

        public override void Init(Meta meta)
        {
            this.meta = meta;
            MetaEvents.OnWebResponse += OnWebResponse;
            meta.Web.Get(address);
        }

        private void OnWebResponse(string address, string json)
        {
            if (this.address == address)
            {//
                meta.data.shop = JsonUtility.FromJson<Shop>(json);
            }
            if (address.StartsWith(addressBuyBanner))
            {
                int banner_id;
                
                bool success = int.TryParse(address.Split("/")[2], out banner_id);
                
                if (success)
                {
                    Debug.Log($"unique_banner_id: {banner_id} json: {json}");
                }
            }
        }
        
        private void BannerClick(int unique_id)
        {
            meta.Web.Get( $"{addressBuyBanner}{unique_id}" );
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                BannerClick(1);
            }
        }
    }
}