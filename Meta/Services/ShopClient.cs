using UnityEngine;

namespace TowerDefence
{
    public class ShopClient : IReceive<OnTryingToBuy>
    {
        private Data data;
        private IReceive<OnTryingToBuy> _receiveImplementation;

        public void Init(Meta meta)
        {
            data = meta.data;
            EventController.Remove(this);
            EventController.Add(this);
        }

        private bool checkTryToBuy(in OnTryingToBuy arg)
        {
            Debug.Log("checkTryToBuy");
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
                Debug.Log($"cash: {gameCurrency.count} - {data.shop.shopSlots[arg.indexInShopList].price}");
                if ( gameCurrency.count > data.shop.shopSlots[arg.indexInShopList].price )
                {
                    Debug.Log($"cash: {gameCurrency.count} - {data.shop.shopSlots[arg.indexInShopList].price}");
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
                    Debug.Log($"realcash: {gameCurrency.count} - {data.shop.shopSlots[arg.indexInShopList].price}");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            Debug.Log($"client: {arg.indexInShopList} - {arg.itemHashCode}");
        }
        
        public void HandleSignal(in OnTryingToBuy arg)
        {
            checkTryToBuy(arg);
        }

        public ShopClient(Meta meta)
        {
            Init(meta);
        }
    }
}
