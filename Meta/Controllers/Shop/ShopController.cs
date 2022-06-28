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

                EventController.Invoke( new OnShopSlotAddNew( meta.data.shop.shopSlots[i], imgPath, i ));
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