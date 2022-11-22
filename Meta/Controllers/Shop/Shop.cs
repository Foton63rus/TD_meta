using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class Shop
    {
        [SerializeField] private List<ShopBanner> _shop_banners = new List<ShopBanner>();

        public Shop( List<ShopBanner> shop_banners )
        {
            _shop_banners = shop_banners;
        }

        public ShopBanner this[int i]
        {
            get { return _shop_banners[i]; }
            set { _shop_banners[i] = value; }
        }
    }
}

