using System;
using System.Collections.Generic;

[Serializable]
public class AllCardsInfo
{    // JSON-класс с описанием всех карт (ДАТА)
    
    //карты
    public List<CardInfo> cards;
    
    public CardInfo this[int id] //индексатор инфы карты по id
    {
        get => cards.Find((x) => x.id == id);
    }
}

[Serializable]
public class CardInfo
{    // Инфа по карте
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