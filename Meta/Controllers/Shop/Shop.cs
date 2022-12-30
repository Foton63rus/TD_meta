using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class Shop
    {
        [SerializeField] public List<ShopBanner> _shop_banners = new List<ShopBanner>();
        [SerializeField] public List<ShopBannerRow> _banner_rows = new List<ShopBannerRow>();

        public Shop( List<ShopBanner> shop_banners, List<ShopBannerRow> banner_rows)
        {
            _shop_banners = shop_banners;
            _banner_rows = banner_rows;
        }

        public ShopBanner this[int i]
        {
            get { return _shop_banners[i]; }
            set { _shop_banners[i] = value; }
        }
    }
}

