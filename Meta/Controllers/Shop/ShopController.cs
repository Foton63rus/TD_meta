using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace TowerDefence
{
    public class ShopController : Controller
    {
        public GameObject UIShopPrefab;
        private GameObject UIShop;
        private ShopView UIShopView;
        private string address;// = "shop";
        private string addressBuyBanner;// = @"shop/buy/";
        private Meta meta;
        private Shop shopinfo;

        private ShopBanner bannerInfo; // временное значение при переборе баннров

        public override void Init(Meta meta)
        {
            this.meta = meta;
            address = meta.data.route.shop;
            addressBuyBanner = meta.data.route.shop_buy_bunner;
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
                            // case "simple": createSimpleBanner(bannerInfo, row);
                            //     break;
                            case "simple": StartCoroutine(createSimpleBanner2(bannerInfo, row)) ;
                                break;
                        }
                    }
                    bannerInfo = null;

                }
            }
        }

        private IEnumerator createSimpleBanner(ShopBanner bannerInfo, GroupBox parent)
        {
            using(UnityWebRequest www = UnityWebRequestTexture.GetTexture(bannerInfo.image))
            {
                //Send Request and wait
                yield return www.SendWebRequest();

                if (www.isHttpError || www.isNetworkError)
                {
                    Debug.Log("Error while Receiving: " + www.error);
                }
                else
                {
                    Debug.Log("Success");

                    //Load Image
                    Texture2D texture2d = null;
                    texture2d = DownloadHandlerTexture.GetContent(www);
                    var sprite = Sprite.Create(texture2d, new Rect(0, 0, 326, texture2d.height), Vector2.zero);
                    
                    int price = math.max(
                        math.max(bannerInfo.price.Coin, bannerInfo.price.Diamond), 
                        math.max(bannerInfo.price.Energy, bannerInfo.price.Star)) ;
                    
                    UIShopView.AddSimpleBanner(parent, 
                        bannerInfo.unique_id,
                        sprite, 
                        price, 
                        bannerInfo.discount, 
                        bannerInfo.count);
                }
            }
        }
        private IEnumerator createSimpleBanner2(ShopBanner bannerInfo, GroupBox parent) //addressables
        {
            string assetId = bannerInfo.image;
            AsyncOperationHandle handle = Addressables.LoadAssetAsync<Sprite>(assetId);
            yield return handle;
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Sprite sprite = (Sprite) handle.Result;
                
                int price = math.max(
                    math.max(bannerInfo.price.Coin, bannerInfo.price.Diamond), 
                    math.max(bannerInfo.price.Energy, bannerInfo.price.Star)) ;
                    
                UIShopView.AddSimpleBanner(parent, 
                    bannerInfo.unique_id,
                    sprite, 
                    price, 
                    bannerInfo.discount, 
                    bannerInfo.count);
            }
        }
    }
}