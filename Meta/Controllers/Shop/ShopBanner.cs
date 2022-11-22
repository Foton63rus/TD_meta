using System;

namespace TowerDefence
{
    [Serializable]
    public class ShopBanner
    {
        public int unique_id;
        public string image;
        public string header;
        public string description;

        public Deposit price;
        public Deposit old_price;

        public Product product;
    }
}