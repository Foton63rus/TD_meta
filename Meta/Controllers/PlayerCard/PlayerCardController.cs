using System;
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
            
            playerDeckView.Init( this, playerCardPrefab );    //Инициализация вьюхи деки
            allPlayerCardsView.Init(this, playerCardPrefab);  //Инициализация вьюхи карт игрока

            loadDeck();
            spawnAllPlayerCards();
        }

        void spawnPlayerCardsInDeck()
        {
            int cardsCount = _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards.Count;

            for (int i = 0; i < cardsCount; i++)
            {
                int localCardID = _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards[i];
                PlayerCard playerCard = _meta.data.playerCards.playerCards[localCardID];
                int globalCardID = playerCard.cardId;
                string imgPath = _meta.data.allCardsInfo[globalCardID].image;

                AddNewCardInDeckToView(globalCardID, imgPath);
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

                    AddNewPlayerCard(i, imgPath);
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

        private void AddNewPlayerCard(int cardID, string imageSource)
        {
            MetaEvents.OnPlayerCardDrawNewOne?.Invoke(new OnPlayerCardDrawNewOneEventArgs(cardID, imageSource));
        }
        private void AddNewCardInDeckToView(int cardID, string imageSource)
        {    //Добавить карту
            MetaEvents.OnPlayerCardInDeckDrawNewOne?.Invoke(new OnPlayerCardDrawNewOneEventArgs(cardID, imageSource));
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
            ClearPlayerDeck();
            spawnPlayerCardsInDeck();
        }

        public void nextDeck()
        {
            int deckID = ++ _meta.data.playerCards.activeDeckID;
            deckID %= _meta.data.playerCards.playerDecks.Count;
            loadDeck(deckID);
        }
    }
}