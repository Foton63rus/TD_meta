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

            spawnShopSlotItems();
        }
        
        void spawnShopSlotItems()
        {
            int slotsCount = meta.data.shop.shopSlots.Count;
            ClearShopSlots();
        
            for (int i = 0; i < slotsCount; i++)
            {
                SlotType visible = meta.data.shop.shopSlots[i].slotType;
                string imgPath;
                if (visible == SlotType.Hidden)
                {
                    imgPath = "Shirt_stripes_01";
                }
                else
                {
                    imgPath = "Shirt_stripes_01"; // тут потом заменить на подгружаемую
                }
                Currency currency = meta.data.shop.shopSlots[i].currency;
                int price = meta.data.shop.shopSlots[i].price;
                CardType cardType = meta.data.shop.shopSlots[i].cardType;
                DeckType deckType = meta.data.shop.shopSlots[i].deckType;

                EventController.Invoke( new OnShopSlotAddNew( 
                    visible,
                    imgPath,
                    currency,
                    price,
                    cardType,
                    deckType));
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