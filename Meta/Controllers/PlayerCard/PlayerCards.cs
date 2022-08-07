using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class PlayerCards
    {    // JSON-класс о картах, которые имеет игрок
        public int activeDeckID;
        public List<PlayerCard> playerCards;
        public List<PlayerDeck> playerDecks;

        public PlayerCard cardByCardIdAndLevel(int _cardId, int _level)
        {// находит карту из имеющихся по карт айди и уровню
            return playerCards.Find((x) => 
                x.cardId == _cardId && x.level == _level);
        }
        
        public void addCardToPlayer(int _cardId, int _level = 0, int _count = 1)
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
            if (playerCards.Find(x => (x.localId == _idFromPlayerCards)) != null)
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
                activeDeckID = index;
            };
        }
    }

    [Serializable]
    public class PlayerCard
    {
        public int localId; //- ID внутри списка карт игрока (те, которые открыл)
        public int cardId;    //- ID в списке всех карт
        public int level;
        public int count;
        

        public PlayerCard(int _localId, int _cardId, int _level = 0, int _count = 1)
        {
            localId = _localId;
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
}
