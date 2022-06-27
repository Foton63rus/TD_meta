using System;
using System.Collections.Generic;

namespace TowerDefence
{
    public class MetaEvents { }
    
    
    //EVENT ARGS
    public struct OnPlayerCardsClearAll{}

    public struct OnPlayerCardDrawNewOne
    {
        public int cardID;
        public string imageSource;

        public OnPlayerCardDrawNewOne(int cardID, string imageSource)
        {
            this.cardID      = cardID;
            this.imageSource = imageSource;
        }
    }

    [Serializable]
    public struct OnShopSlotAddNew
    {
        public SlotType visible;
        public string imgPath;
        public Currency currency;
        public int price;
        public CardType cardType;
        public DeckType deckType;

        public OnShopSlotAddNew(SlotType visible, string imgPath, Currency currency, int price, CardType cardType, DeckType deckType)
        {
            this.visible  = visible;
            this.imgPath  = imgPath;
            this.currency = currency;
            this.price    = price;
            this.cardType = cardType;
            this.deckType = deckType;
        }
    }
}
