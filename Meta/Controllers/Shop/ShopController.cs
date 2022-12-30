using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefence
{
    public class ShopController : Controller
    {
        public GameObject UIShopPrefab;
        private GameObject UIShop;
        private ShopView UIShopView;
        private string address = "shop";
        private string addressBuyBanner = @"shop/buy/";
        private Meta meta;
        private Shop shopinfo;

        private ShopBanner bannerInfo; // временное значение при переборе баннров

        public override void Init(Meta meta)
        {
            this.meta = meta;
            InitUIShop();
            
            MetaEvents.OnWebResponse += OnWebResponse;
            MetaEvents.BannerClicked += BannerClick;
            meta.Web.Get(address);
        }

        private void InitUIShop()
        {
            UIShop = Instantiate(UIShopPrefab);
            UIShopView = UIShop.GetComponent<ShopView>();
        }

        private void OnWebResponse(string address, string json)
        {
            if (this.address == address)
            {//
                ReadShopInfo(json);
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

        private void ReadShopInfo(string json)
        {
            shopinfo = JsonUtility.FromJson<Shop>(json);
            meta.data.shop = shopinfo;

            for (int i = 0; i < shopinfo._banner_rows.Count; i++)
            {
                GroupBox row = UIShopView.AddRowForBanners();
                for (int j = 0; j < shopinfo._banner_rows[i].Row.Count; j++)
                {
                    int bannerId = shopinfo._banner_rows[i].Row[j];
                    bannerInfo = shopinfo._shop_banners.First(x => x.unique_id == bannerId);
                    if (bannerInfo != null)
                    {
                        switch (bannerInfo.type)
                        {
                            case "simple": createSimpleBanner(bannerInfo, row);
                                break;
                        }
                    }
                    bannerInfo = null;

                }
            }
        }

        private void createSimpleBanner(ShopBanner bannerInfo, GroupBox parent)
        {
            int price = math.max(
                math.max(bannerInfo.price.Coin, bannerInfo.price.Diamond), 
                math.max(bannerInfo.price.Energy, bannerInfo.price.Star)) ;
            UIShopView.AddBanner(parent, 
                bannerInfo.unique_id,
                Resources.Load<Sprite>("Tower_001"), 
                price, 
                bannerInfo.discount, 
                bannerInfo.count);
        }
    }
}