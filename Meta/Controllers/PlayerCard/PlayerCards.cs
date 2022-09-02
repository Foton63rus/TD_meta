using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class PlayerCards
    {    // JSON-класс о картах, которые имеет игрок
        public int activeDeckID;
        public List<PlayerCard> playerCards;
        public List<PlayerDeck> playerDecks;

        public readonly int[] minCount4LvlUp = new int[]{2, 3, 4, 5};

        public PlayerCard this[int UID]
        {
            get { return playerCards.Find(x => x.UID == UID); }
        }

        public PlayerCard GetCardByCardIdAndLevel(int _cardId, int _level)
        {// находит карту из имеющихся по карт айди и уровню
            return this.playerCards.Find((x) => 
                x.cardId == _cardId && x.level == _level);
        }
        
        public PlayerCard addCardToPlayer(int _cardId, int _level = 0)
        {   //добавляем карты игроку
            PlayerCard newCard = new PlayerCard(playerCards.Max( x => x.UID)+1 , _cardId, _level);
            playerCards.Add(newCard);
            Debug.Log($"added card id: {newCard.cardId}, lvl: {newCard.level}, uid: {newCard.UID}");
            return newCard;
        }

        public List<PlayerCard> GetCardsByGID(int GID)
        {
            return playerCards.FindAll(x => x.cardId == GID);
        }

        public List<PlayerCard> GetCardsByUID(int UID)
        {
            PlayerCard thisCard = playerCards.Find(x => x.UID == UID);
            if (thisCard != null)
            {
                return GetCardsByGID(thisCard.cardId);
            }
            else
            {
                return null;
            }
        }

        public int GetCountByGID(int GID)
        {
            return GetCardsByGID(GID).Count;
        }

        public int GetCountByUID(int UID)
        {
            return GetCardsByUID(UID).Count;
        }
        
        private List<PlayerCard> playerCardsSortedByLevel()
        {
            return playerCards.OrderByDescending(x => x.level).OrderBy(x => x.cardId).ToList();
        }

        public void RefreshCurrentDeck()
        {
            //Удаляем карты, UID которых отсутствует в playerCards
            playerDecks[activeDeckID].cards.RemoveAll(x => !playerCards.Exists(y => y.UID == x));
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
        public int UID; //- ID внутри списка карт игрока (те, которые открыл)
        public int cardId;    //- ID в списке всех карт
        public int level;

        public PlayerCard(int _uid, int _cardId, int _level = 0)
        {
            UID = _uid;
            cardId = _cardId;
            level = _level;
        }
    }
        
    [Serializable]
    public class PlayerDeck
    {
        public int deckId;    //localId
        public DeckType deckType;
        public List<int> cards;

        public void addCard(int uid)
        {
            cards.Add(uid);
        }

        public PlayerDeck(int _deckId, DeckType _deckType)
        {
            deckId = _deckId;
            deckType = _deckType;
            cards = new List<int>();
        }
    }
}
