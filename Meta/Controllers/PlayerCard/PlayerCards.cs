using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class PlayerCards
    {    // JSON-класс о картах, которые имеет игрок
        public int activeDeckID;
        public List<PlayerCard> playerCards;
        public List<PlayerDeck> playerDecks;

        public readonly int[] minCount4LvlUp = new int[]{3, 2, 3, 5};

        public PlayerCard cardByCardIdAndLevel(int _cardId, int _level)
        {// находит карту из имеющихся по карт айди и уровню
            return playerCards.Find((x) => 
                x.cardId == _cardId && x.level == _level);
        }
        
        public void addCardToPlayer(int _cardId, int _level = 0, int _count = 1)
        {    //добавляем карты игроку, если с таким id и уровнем уже существуют, то добавляем нужное кол-во
            PlayerCard cardIfPlayerHave = cardByCardIdAndLevel(_cardId, _level);
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
                upgradeCardToNextLvl(cardIfPlayerHave.localId, cardIfPlayerHave.level);
            }
        }

        public void upgradeCardToNextLvl(int localId, int currentLevel)
        {
            if (currentLevel < minCount4LvlUp.Length - 1)
            {
                int minimumForUpgrade = minCount4LvlUp[currentLevel];
                var cardWithConditions = playerCards.Find(
                    x => x.localId == localId && x.level == currentLevel && x.count > minimumForUpgrade - 1);

                if (cardWithConditions != null)
                {
                    Debug.Log($"id {cardWithConditions.cardId}, lvl {cardWithConditions.level}");
                    playerCards[localId].count -= minimumForUpgrade;
                    addCardToPlayer( cardWithConditions.cardId, currentLevel+1 );
                    Debug.Log($"id {cardWithConditions.cardId}, lvl {cardWithConditions.level}");
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

        public void addCardToDeck( CardInfo cardInfo, int deckId)
        {
            if (playerDecks[deckId] == null)
            {
                Debug.Log("public void addCardToDeck( int cardId, int deckId): deck with this deckId not Exist");
            }
            else
            {
                PlayerCard addedCard = playerCards.Find(x => x.cardId == cardInfo.id);
                if (addedCard == null)
                {
                    Debug.Log("public void addCardToDeck( int cardId, int deckId): card with this cardId not Exist");
                }
                else
                {
                    if ( cardInfo.deckType == playerDecks[deckId].deckType || 
                         playerDecks[deckId].deckType == DeckType.Common)
                    {
                        playerDecks[deckId].addCard(addedCard.localId);
                        Debug.Log($"added card lId: {addedCard.localId} to deck {deckId}");
                        //todo разобраться с апгрейдом и добавлением в колоду
                    }
                    else
                    {
                        Debug.Log($"cardInfo.deckType != playerDecks[deckId].deckType");
                    }
                }
            }
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
