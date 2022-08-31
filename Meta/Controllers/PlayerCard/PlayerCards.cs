using System;
using System.Collections.Generic;
using System.Linq;

namespace TowerDefence
{
    [Serializable]
    public class PlayerCards
    {    // JSON-класс о картах, которые имеет игрок
        public int activeDeckID;
        public List<PlayerCard> playerCards;
        public List<PlayerDeck> playerDecks;

        public readonly int[] minCount4LvlUp = new int[]{2, 3, 4, 5};

        public PlayerCard GetCardByCardIdAndLevel(int _cardId, int _level)
        {// находит карту из имеющихся по карт айди и уровню
            return playerCards.Find((x) => 
                x.cardId == _cardId && x.level == _level);
        }
        
        public void addCardToPlayer(int _cardId, int _level = 0, int _count = 1)
        {   //добавляем карты игроку, если с таким id и уровнем уже существуют, то добавляем нужное кол-во
            PlayerCard cardIfPlayerHave = GetCardByCardIdAndLevel(_cardId, _level);
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

        private List<PlayerCard> playerCardsSortedByLevel()
        {
            return playerCards.OrderByDescending(x => x.level).OrderBy(x => x.cardId).Where(x => x.count != 0).ToList();
        }

        public void SortCurrentDeck()
        {
            List<PlayerCard> playerCardsSortedByLevel = this.playerCardsSortedByLevel();
            List<int> tmpDeck = new List<int>(); 
            List<int> sortedDeck = new List<int>();
            
            //формируется список временной колоды с глобальными id, чтобы искать повторения
            foreach (int localID in playerDecks[activeDeckID].cards)
            {    
                PlayerCard card = playerCards.Find(x => x.localId == localID);
                if (card != null)
                {
                    tmpDeck.Add(card.cardId); 
                }
            }

            foreach (int GID in tmpDeck)
            {
                PlayerCard card = playerCardsSortedByLevel.Find(x => x.cardId == GID);
                if (card.count > 1)
                {
                    card.count--;
                }
                else // card.count == 1, меньше не может быть, так как тут удаляем, а до этого отфильтровали в playerCardsSortedByLevel() по count == 0
                {
                    playerCardsSortedByLevel.Remove(card);
                }
                sortedDeck.Add( card.localId );
            }
            playerDecks[activeDeckID].cards.Clear();
            playerDecks[activeDeckID].cards.AddRange( sortedDeck );
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
        public int deckId;    //localId
        public DeckType deckType;
        public List<int> cards;

        public void addCard(int localId)
        {
            cards.Add(localId);
        }

        public PlayerDeck(int _deckId, DeckType _deckType)
        {
            deckId = _deckId;
            deckType = _deckType;
            cards = new List<int>();
        }
    }
}
