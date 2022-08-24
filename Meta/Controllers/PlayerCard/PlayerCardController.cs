using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence
{
    public class PlayerCardController : Controller
    {
        private Meta _meta;
        [SerializeField] private PlayerDeckView playerDeckView;    // Контейнер UI для карт в текущей колоде
        [SerializeField] private AllPlayerCardsView allPlayerCardsView;    // Контейнер UI для карт в текущей колоде
        [SerializeField] private GameObject playerCardPrefab;
        [SerializeField] private TextAsset allCardsInfoAsset;
        [SerializeField] private TextAsset playerCardsAsset;

        public override void Init( Meta meta)
        {
            _meta = meta;
            
            meta.data.allCardsInfo = JsonUtility.FromJson<AllCardsInfo>(allCardsInfoAsset.text);
            meta.data.playerCards = JsonUtility.FromJson<PlayerCards>(playerCardsAsset.text);

            MetaEvents.OnPlayerCardAdd += addNewCard;
            MetaEvents.OnTryAddCardToCurrentDeck += addCardToCurrentDeck;
            
            playerDeckView.Init( this, playerCardPrefab );    //Инициализация вьюхи деки
            allPlayerCardsView.Init(this, playerCardPrefab);  //Инициализация вьюхи карт игрока

            loadDeck();
            spawnAllPlayerCards();
        }

        void spawnPlayerCardsInDeck()
        {
            cleanEmptyCardsInDeck();
            ClearPlayerDeck();
            int cardsCount = _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards.Count;

            for (int i = 0; i < cardsCount; i++)
            {
                int localCardID = _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards[i];
                PlayerCard playerCard = _meta.data.playerCards.playerCards[localCardID];
                int globalCardID = playerCard.cardId;
                string imgPath = _meta.data.allCardsInfo[globalCardID].image;

                AddNewCardInDeckToView(playerCard, imgPath);
            }
        }

        public void spawnAllPlayerCards()
        {
            int cardsCount = _meta.data.playerCards.playerCards.Count;
            for (int i = 0; i < cardsCount; i++)
            {
                if (_meta.data.playerCards.playerCards[i].count>0)
                {
                    int id = _meta.data.playerCards.playerCards[i].cardId;
                    string imgPath = _meta.data.allCardsInfo[id].image;

                    AddNewPlayerCard(_meta.data.playerCards.playerCards[i], imgPath);
                }
            }
        }

        public void refreshPlayerCards()
        {
            MetaEvents.OnPlayerCardClearView?.Invoke();
            spawnAllPlayerCards();
        }

        private void addNewCard( CardInfo cardInfo )    //добавление карты например при покупке
        {
            _meta.data.playerCards.addCardToPlayer(cardInfo.id);
            refreshPlayerCards();
        }

        private void AddNewPlayerCard(PlayerCard playerCard, string imageSource)
        {
            MetaEvents.OnPlayerCardDrawNewOne?.Invoke(new OnPlayerCardDrawNewOneEventArgs(playerCard, imageSource));
        }
        private void AddNewCardInDeckToView(PlayerCard playerCard, string imageSource)
        {    //Добавить карту
            MetaEvents.OnPlayerCardInDeckDrawNewOne?.Invoke(new OnPlayerCardDrawNewOneEventArgs(playerCard, imageSource));
        }
        private void ClearPlayerDeck()
        {    //почистить список карт в колоде
            MetaEvents.OnPlayerDeckClearAll?.Invoke();
        }

        public void loadDeck(int deckID = 0)
        {
            if (deckID >= 0 && deckID < _meta.data.playerCards.playerDecks.Count)
            {
                _meta.data.playerCards.activeDeckID = deckID;
            }
            spawnPlayerCardsInDeck();
        }

        private void cleanEmptyCardsInDeck()
        {
            int cardsCount = _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards.Count;

            for (int i = cardsCount - 1; i >= 0; i--)
            {
                int localCardID = _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards[i];
                PlayerCard playerCard = _meta.data.playerCards.playerCards[localCardID];
                if (playerCard.count < 1)
                {
                    _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards.Remove(i);
                }
            }
        }

        public void addCardToDeck(int localID, int deckID)
        {
            PlayerCard addedCard;
            if (_meta.data.playerCards.playerCards.Count-1>=localID && 
                _meta.data.playerCards.playerDecks.Count-1>=deckID)
            {
                addedCard = _meta.data.playerCards.playerCards[localID];
            }
            else
            {
                return;
            }
            CardInfo cardInfo = _meta.data.allCardsInfo[addedCard.cardId];
            if (_meta.data.playerCards.playerDecks[deckID] == null)
            {
                Debug.Log("public void addCardToDeck( int cardId, int deckId): deck with this deckId not Exist");
                return;
            }
            if (addedCard == null)
            {
                Debug.Log("public void addCardToDeck( int cardId, int deckId): card with this cardId not Exist");
                return;
            }

            if (addedCard.count <= 0)
            {
                Debug.Log("public void addCardToDeck( int cardId, int deckId): count of cards <= 0");
                return;
            }
            if ( !(cardInfo.deckType == _meta.data.playerCards.playerDecks[deckID].deckType || 
                 _meta.data.playerCards.playerDecks[deckID].deckType == DeckType.Common))
            {
                Debug.Log($"cardInfo.deckType != playerDecks[deckId].deckType");
                return;
            }

            int countOfThisCardInDeck = _meta.data.playerCards.playerDecks[deckID].cards.Where(x => x == localID).Count();
            if (countOfThisCardInDeck+1 > _meta.data.playerCards.playerCards[localID].count)
            {
                Debug.Log($"добавлено максимальное количество карт");
                return;
            }
            Debug.Log($"cur count: {countOfThisCardInDeck}");
            _meta.data.playerCards.playerDecks[deckID].addCard(addedCard.localId);
            Debug.Log($"added card lId: {addedCard.localId} to deck {deckID}");
            spawnPlayerCardsInDeck();
        }
        
        public void addCardToCurrentDeck(int localID)
        {
            addCardToDeck(localID, _meta.data.playerCards.activeDeckID);
        }

        public void nextDeck()
        {
            int deckID = ++ _meta.data.playerCards.activeDeckID;
            deckID %= _meta.data.playerCards.playerDecks.Count;
            loadDeck(deckID);
        }
    }
}