using System;
using System.Collections.Generic;

namespace TowerDefence
{
    [Serializable]
    public class ShopBanner
    {
        public string type;
        public int unique_id;
        public int count;
        public string image;
        public string header;
        public string description;

        public Deposit price;
        public string discount;
        public Deposit old_price;

        public Product product;
    }

    [Serializable]
    public class ShopBannerRow
    {
        public List<int> Row = new List<int>();

        public int this[int i]
        {
            get { return Row[i];}
            private set { Row[i] = value; }
        }

        public int Count => Row.Count;
    }
}