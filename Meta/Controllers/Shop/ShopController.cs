using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class ShopController : Controller
    {
        private Meta meta;
        private ShopClient client;
        [SerializeField] private ShopView view;
        [SerializeField] private GameObject ShopSlotPrefab;
        [SerializeField] private TextAsset jsonShopAsset;
        
        public Shop shop;

        public override void Init(Meta meta)
        {
            this.meta = meta;
            client = new ShopClient(meta);
            meta.data.shop = JsonUtility.FromJson<Shop>(jsonShopAsset.text);
            view.Init(this, ShopSlotPrefab);

            spawnShopSlotItems();
            
            ShopEvents.OnTryingToBuy -= buyCard;
            ShopEvents.OnTryingToBuy += buyCard;
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
                AddNewSlotItem( new OnShopSlotAddNewEventArgs( meta.data.shop.shopSlots[i], imgPath, i ));
            }
        }

        private void AddNewSlotItem(OnShopSlotAddNewEventArgs args)
        {    //Добавить карту
            ShopEvents.OnShopSlotAddNew?.Invoke( args);
        }
        private void ClearShopSlots()
        {    //почистить слоты магазина
            ShopEvents.OnShopSlotsClearAll?.Invoke();
        }

        public void buyCard(OnTryingToBuyEventArgs args)
        {
            CardShopSlot slot = meta.data.shop.shopSlots[args.indexInShopList];
            var currency = meta.data.gameCurrency;
            
            if (slot.currency == Currency.Free && 
                meta.data.gameCurrency[0]>0)
            {
                currency[0] = currency[0] - 1;
                Debug.Log($"{currency[0]}" );
            }
            else if (slot.currency == Currency.Ads && 
                     currency[1]>0)
            {
                currency[1] = currency[1] - 1;
                Debug.Log($"{currency[1]}" );
            }
            else if (slot.currency == Currency.GameMoney && 
                     currency[2] >= slot.price)
            {
                currency[2] = currency[2] - slot.price;
                Debug.Log($"{currency[2]}" );
            }
            else if (slot.currency == Currency.RealMoney && 
                     currency[3] >= slot.price)
            {
                currency[3] = currency[3] - slot.price;
                Debug.Log($"{currency[3]}" );
            }
            
            //if (currency[ Enum.GetValues( typeof(Currency))[1] ] >= slot.price)
            return;
        }
    }
}