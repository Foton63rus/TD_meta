using UnityEngine;

namespace TowerDefence
{
    public class ShopClient
    {
        private Data data;

        public void Init(Meta meta)
        {
            data = meta.data;
            //ShopEvents.OnTryingToBuy -= OnTryingToBuyHandler;
            //ShopEvents.OnTryingToBuy += OnTryingToBuyHandler;
        }
        public ShopClient(Meta meta)
        {
            Init(meta);
        }
    }
}
