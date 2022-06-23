using System;
using System.Collections.Generic;

namespace TowerDefence
{
    [Serializable]
    public class Shop
    {    // JSON-Класс магазина (ДАТА)
    
        //ячейки в магазине
        public List<CardShopSlot> shopSlots;
    }

    [Serializable]
    public class CardShopSlot
    {    //ячейка
        public SlotType slotType;    // visible = 1 | hidden = 0 - содержание ячейки видно (лицевая часть) или нет (рубашка)
        public DeckType deckType;    // тип колоды Common = 0, People = 1, Mag = 2, Mech = 3
        public CardType cardType;    // тип карты Tower = 0, Unit = 1, Hero = 2, Spell = 3
        public Currency currency;    // Валюта Free = 0, Ads = 1, GameMoney = 2, RealMoney = 3
        public int price;            // Цена
    }

    [Serializable]
    public enum SlotType
    {
        Hidden = 0,
        Visible = 1
    }

    [Serializable]
    public enum Currency
    {
        Free = 0,
        Ads = 1,
        GameMoney = 2,
        RealMoney = 3
    }

    [Serializable]
    public class CardsSet
    {
        public CardRarity cardRarity;
        public int number;
    }
}
