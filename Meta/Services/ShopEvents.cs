using System;

namespace TowerDefence
{
    public class ShopEvents { }
    
    //При событии добавления слота в магазине (отрисовка)
    [Serializable] public struct OnShopSlotAddNew
    {
        public CardShopSlot shopSlot;
        public string imgPath;
        public int indexInShopSlot;

        public OnShopSlotAddNew(CardShopSlot shopSlot, string imgPath, int indexInShopSlot)
        {
            this.shopSlot        = shopSlot;
            this.imgPath         = imgPath;
            this.indexInShopSlot = indexInShopSlot;
        }
    }
    
    public struct OnTryingToBuy
    {
        public int indexInShopList;
        public int itemHashCode;

        public OnTryingToBuy( int indexInShopList, int itemHashCode)
        {
            this.indexInShopList = indexInShopList;
            this.itemHashCode    = itemHashCode;
        }
    }
    
    public struct GetBuyCard
    {
        
    }
}
