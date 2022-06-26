using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class ShopController : Controller
    {
        private Meta meta;
        [SerializeField] private ShopView view;
        [SerializeField] private GameObject ShopSlotPrefab;
        [SerializeField] private TextAsset jsonShopAsset;
        
        public Shop shop;

        public override void Init(Meta meta)
        {
            this.meta = meta;
            meta.data.shop = JsonUtility.FromJson<Shop>(jsonShopAsset.text);
            view.Init(this, ShopSlotPrefab);
            
        }
        
        void spawnShopSlotItems()
        {
            int slotsCount = meta.data.shop.shopSlots.Count;
            ClearShopSlots();
        
            for (int i = 0; i < slotsCount; i++)
            {
                /*int localCardID = meta.data.playerCards.playerDecks[meta.data.playerCards.activeDeck].cards[i];
                PlayerCard playerCard = meta.data.playerCards.playerCards[localCardID];
                int globalCardID = playerCard.cardId;
                string imgPath = meta.data.allCardsInfo[globalCardID].image;

                AddNewSlotItem(globalCardID, imgPath);*/
            }
        }

        private void AddNewSlotItem(int cardID, string imageSource)
        {    //Добавить карту
            //onPlayerCardDrawNewOne?.Invoke(cardID, imageSource);
        }
        private void ClearShopSlots()
        {    //почистить слоты магазина
            //onPlayerCardsClearAll?.Invoke();
        }
    }
}