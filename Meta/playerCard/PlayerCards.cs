using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCards
{    // JSON-класс о картах, которые имеет игрок
    public int activeDeck;
    public List<PlayerCard> playerCards;
    public List<PlayerDeck> playerDecks;

    public PlayerCard cardByCardIdAndLevel(int _cardId, int _level)
    {// находит карту из имеющихся по карт айди и уровню
        return playerCards.Find((x) => 
            x.cardId == _cardId && x.level == _level);
    }
    
    public void addCardToPlayer(int _cardId, int _level = 1, int _count = 1)
    {    //добавляем карты игроку, если с таким id и уровнем уже существуют, то добавляем нужное кол-во
        var cardIfPlayerHave = cardByCardIdAndLevel(_cardId, _level);
        if ( cardIfPlayerHave is null )
        {
            playerCards.Add(new PlayerCard(
                playerCards.Count,
                _cardId,
                _level,
                _count)); 
        }
        else
        {
            cardIfPlayerHave.count += _count;
        }
    }

    public void addCartToDeck(int _idFromPlayerCards, int _deckId)
    {    //добавляет карту, которая открыта у игрока, в колоду, если таковая существует
        if (playerCards.Find(x => (x.id == _idFromPlayerCards)) != null)
        {
            PlayerDeck deck = playerDecks.Find(x => x.id == _deckId);
            if (deck == null)
            {
                return;
            }
            else
            {
                deck.addCard(_idFromPlayerCards);
            }
        } 
    }

    public void addDeck(DeckType _deckType)
    {    //добавляем колоду
        playerDecks.Add(
            new PlayerDeck(playerDecks.Count, _deckType));
    }

    public void SetActiveDeck(int index = 0)
    {
        if (index <= playerDecks.Count - 1 && index >= 0)
        {
            activeDeck = index;
        };
    }
}

[Serializable]
public class PlayerCard
{
    public int id; //- ID внутри списка карт игрока (те, которые открыл)
    public int cardId;    //- ID в списке всех карт
    public int level;
    public int count;
    

    public PlayerCard(int _id, int _cardId, int _level = 1, int _count = 1)
    {
        id = _id;
        cardId = _cardId;
        level = _level;
        count = _count;
    }
}
    
[Serializable]
public class PlayerDeck
{
    public int id;
    public DeckType deckType;
    public List<int> cards;

    public void addCard(int _idFromPlayerCards)
    {
        cards.Add(_idFromPlayerCards);
    }

    public PlayerDeck(int _id, DeckType _deckType)
    {
        id = _id;
        deckType = _deckType;
        cards = new List<int>();
    }
}

/*[Serializable]
public class Deck
{
    public AllCardsInfo allCardsInfo;
    
    public List<int> cards;
    public DeckType type;

    public void addCard(int id)
    {
        if (type == DeckType.Common || allCardsInfo[id].deckType == type )
        {
            
        }
    }

    public void deleteCard()
    {
        
    }
}*/
