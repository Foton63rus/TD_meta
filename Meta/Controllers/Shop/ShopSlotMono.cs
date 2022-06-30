using UnityEngine;

namespace TowerDefence
{
    public class ShopSlotMono : MonoBehaviour
    {
        public GameObject goHeader;
        public GameObject goFooterPrice;
        public GameObject goCurrency;
        
        public OnShopSlotAddNewEventArgs slotInfo;
        public int indexInShopSlot;
    }
}
