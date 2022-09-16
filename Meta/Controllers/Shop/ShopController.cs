using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefence
{
    public class ShopController : Controller
    {
        private Meta meta;
        [SerializeField] private ShopView view;
        [SerializeField] private GameObject ShopSlotPrefab;
        [SerializeField] private TextAsset jsonShopAsset;

        public override void Init(Meta meta)
        {
            this.meta = meta;
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

            if ( (slot.currency == Currency.Free || slot.currency == Currency.Ads) && 
                 currency[(int) slot.currency] > 0)
            {
                currency[(int) slot.currency] = currency[(int) slot.currency] - 1;
                randomCardByDeck( slot );
            }
            else if ( (slot.currency == Currency.GameMoney || slot.currency == Currency.RealMoney) && 
                      currency[(int) slot.currency] >= slot.price)
            {
                currency[(int) slot.currency] = currency[(int) slot.currency] - slot.price;
                randomCardByDeck( slot );
            }
        }

        private CardInfo randomCardByDeck( CardShopSlot slot )
        {
            List<CardInfo> allCardsWithBuyingConditions = meta.data.allCardsInfo.cards.FindAll(x => x.deckType == slot.deckType);
            if (allCardsWithBuyingConditions.Count > 0)
            {
                int rndIndex = Random.Range(0, allCardsWithBuyingConditions.Count);
                CardInfo randomCard = allCardsWithBuyingConditions[rndIndex];
                Debug.Log($"card: {randomCard.id} decktype: {randomCard.deckType} {randomCard.image}");
                MetaEvents.OnPlayerCardAdd.Invoke( randomCard );
                return randomCard;
            }
            else
            {    //Debug.Log("Не нашлось карт с таким типом колоды");
                MetaEvents.OnPlayerCardAdd.Invoke( null );
                return null;
            }
        }
    }
}