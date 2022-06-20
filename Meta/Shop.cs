using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Shop
{
    public List<CardShopSlot> shopSlots;
}

[Serializable]
public class CardShopSlot
{
    public SlotType slotType;
    public DeckType deckType;
    public CardType cardType;
    public Currency currency;
    public int price;
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