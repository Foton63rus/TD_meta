using UnityEngine;

namespace TowerDefence
{
    public class ShopClient
    {
        private Data data;

        public void Init(Meta meta)
        {
            data = meta.data;
            ShopEvents.OnTryingToBuy -= OnTryingToBuyHandler;
            ShopEvents.OnTryingToBuy += OnTryingToBuyHandler;
        }

        private bool checkTryToBuy(OnTryingToBuyEventArgs arg)
        {
            
            if (data.shop.shopSlots[arg.indexInShopList].currency == Currency.Free)
            {
                return true;
            }
            else if (data.shop.shopSlots[arg.indexInShopList].currency == Currency.Ads)
            {
                return true;
            }
            else if (data.shop.shopSlots[arg.indexInShopList].currency == Currency.GameMoney)
            {
                GameCurrency gameCurrency = data.gameCurrency.Find(x => x.currency == Currency.GameMoney);
                
                if ( gameCurrency.count > data.shop.shopSlots[arg.indexInShopList].price )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else //if (data.shop.shopSlots[arg.indexInShopList].currency == Currency.RealMoney)
            {
                GameCurrency gameCurrency = data.gameCurrency.Find(x => x.currency == Currency.RealMoney);
                if ( gameCurrency.count > data.shop.shopSlots[arg.indexInShopList].price )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        public void OnTryingToBuyHandler( OnTryingToBuyEventArgs arg)
        {
            Debug.Log($" {arg.itemHashCode} ");
            if (checkTryToBuy(arg))
            {
                //EventController.Invoke( arg );
            }
            checkTryToBuy(arg);
        }

        public ShopClient(Meta meta)
        {
            Init(meta);
        }
    }
}
