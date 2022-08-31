using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence
{
    public class PlayerCardController : Controller
    {
        private Meta _meta;
        public PlayerCardCommandConfigurator commandConfigurator;
        [SerializeField] private PlayerDeckView playerDeckView;    // Контейнер UI для карт в текущей колоде
        [SerializeField] private AllPlayerCardsView allPlayerCardsView;    // Контейнер UI для карт в текущей колоде
        [SerializeField] private GameObject playerCardPrefab;
        [SerializeField] private TextAsset allCardsInfoAsset;
        [SerializeField] private TextAsset playerCardsAsset;

        public override void Init( Meta meta)
        {
            _meta = meta;
            commandConfigurator = new PlayerCardCommandConfigurator();
            
            meta.data.allCardsInfo = JsonUtility.FromJson<AllCardsInfo>(allCardsInfoAsset.text);
            meta.data.playerCards = JsonUtility.FromJson<PlayerCards>(playerCardsAsset.text);

            MetaEvents.OnPlayerCardAdd += addNewCard;
            MetaEvents.OnRemoveCardFromDeck += removeCardFromCurrentDeck;
            
            playerDeckView.Init(this, commandConfigurator, playerCardPrefab );    //Инициализация вьюхи деки
            allPlayerCardsView.Init(this, commandConfigurator, playerCardPrefab);  //Инициализация вьюхи карт игрока

            loadDeck();
            spawnAllPlayerCards();
        }

        void spawnPlayerCardsInDeck()
        {
            _meta.data.playerCards.SortCurrentDeck();
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

        public void upgradeCard(int localId)
        {
            PlayerCard currentCard = _meta.data.playerCards.playerCards.Find(x => x.localId == localId);
            int currentLevel = currentCard.level;

            if (currentLevel <= _meta.data.playerCards.minCount4LvlUp.Length - 1)
            {
                int minCount4Upgrade = _meta.data.playerCards.minCount4LvlUp[currentCard.level];
                int currentCardCount = currentCard.count;

                if (currentCardCount >= minCount4Upgrade)
                {
                    currentCard.count = currentCardCount - minCount4Upgrade;
                    _meta.data.playerCards.addCardToPlayer(localId, currentLevel+1);
                    refreshPlayerCards();
                    spawnPlayerCardsInDeck();
                    Debug.Log("апгрейд !!!");
                }
                else
                {
                    Debug.Log("недостаточно карт для апгрейда");
                }
            }
            else
            {
                Debug.Log("максимальный уровень");
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
            _meta.data.playerCards.SortCurrentDeck();
            spawnPlayerCardsInDeck();
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

        public void removeCardFromDeck(int localID, int deckID)
        {
            if (_meta.data.playerCards.playerCards.Count-1<localID && 
                _meta.data.playerCards.playerDecks.Count-1<deckID)
            {
                return;
                //new IndexOutOfRangeException("public void removeCardFromDeck(int localID, int deckID) : index out of range");
            }
            _meta.data.playerCards.playerDecks[deckID].cards.Remove(localID);
            spawnPlayerCardsInDeck();
        }

        public void removeCardFromCurrentDeck(int localID)
        {
            removeCardFromDeck(localID, _meta.data.playerCards.activeDeckID);
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