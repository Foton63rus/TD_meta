using System;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence
{
    public static class ShopEvents
    {

        public static UnityAction OnShopSlotsClearAll;
        //Событие добавления слота в магазине (отрисовка)
        public static UnityAction<OnShopSlotAddNewEventArgs> OnShopSlotAddNew;
        //Событие попытки купить карту (вызов для проверки на клиенте)
        public static UnityAction<OnTryingToBuyEventArgs> OnTryingToBuy;
        //Событие попытки купить карту (отправка GET запроса на сервер для проверки)
        public static UnityAction<GetBuyCardEventArgs> GetBuyCard;
    }
    
    [Serializable] public struct OnShopSlotAddNewEventArgs
    {
        public CardShopSlot shopSlot;
        public string imgPath;
        public int indexInShopSlot;

        public OnShopSlotAddNewEventArgs(CardShopSlot shopSlot, string imgPath, int indexInShopSlot)
        {
            this.shopSlot        = shopSlot;
            this.imgPath         = imgPath;
            this.indexInShopSlot = indexInShopSlot;
        }
    }
    
    public struct OnTryingToBuyEventArgs
    {
        public int indexInShopList;
        public int itemHashCode;

        public OnTryingToBuyEventArgs( int indexInShopList, int itemHashCode)
        {
            this.indexInShopList = indexInShopList;
            this.itemHashCode    = itemHashCode;
        }
    }
    
    public struct GetBuyCardEventArgs
    {
        public int indexInShopList;
        public int itemHashCode;

        public GetBuyCardEventArgs( OnTryingToBuyEventArgs arg)
        {
            indexInShopList = arg.indexInShopList;
            itemHashCode    = arg.itemHashCode;
            Debug.Log("OTTB");
        }
    }
}
