using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class AllCardsInfo
{
    public List<CardInfo> cards;

    public CardInfo this[int id] //выдает инфу карты по id
    {
        get
        {
            return cards.Find((x) => x.id == id);
        }
        //set { cards[i] = value; }
    }
}

[Serializable]
public class CardInfo
{
    public int id;
    public string image;
    public DeckType deckType;
    public CardType cardType;
    public CardRarity rarity;
    public int[] costByLevel;
}

public enum DeckType{
    Common = 0,
    People = 1,
    Mag = 2,
    Mech = 3
}

public enum CardType
{
    Tower = 0,
    Unit = 1,
    Hero = 2,
    Spell = 3
}

public enum CardRarity
{
    Common = 0,
    Rare = 1,
    SuperRare = 2,
    Epic = 3,
    Legendary = 4
}