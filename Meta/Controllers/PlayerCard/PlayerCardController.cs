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
        
        [SerializeField] private List<int> upgradingdcards = new List<int>();

        public override void Init( Meta meta)
        {
            _meta = meta;
            commandConfigurator = new PlayerCardCommandConfigurator();
            
            meta.data.allCardsInfo = JsonUtility.FromJson<AllCardsInfo>(allCardsInfoAsset.text);
            meta.data.playerCards = JsonUtility.FromJson<PlayerCards>(playerCardsAsset.text);

            MetaEvents.OnPlayerCardAdd += addNewCard;
            MetaEvents.OnRemoveCardFromDeck += removeCardFromCurrentDeck;
            
            playerDeckView?.Init(this, commandConfigurator, playerCardPrefab??null );    //Инициализация вьюхи деки
            allPlayerCardsView?.Init(this, commandConfigurator, playerCardPrefab??null);  //Инициализация вьюхи карт игрока

            loadDeck();
            spawnAllPlayerCards();
        }

        void spawnPlayerCardsInDeck()
        {
            _meta.data.playerCards.RefreshCurrentDeck();
            ClearPlayerDeck();
            int cardsCount = _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards.Count;
            
            _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards.RemoveAll( 
                x => !_meta.data.playerCards.playerCards.Exists(y => y.UID == x));

            for (int i = 0; i < cardsCount; i++)
            {
                int uid = _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards[i];
                
                PlayerCard playerCard = _meta.data.playerCards[uid];
                
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
                int id = _meta.data.playerCards.playerCards[i].cardId;
                string imgPath = _meta.data.allCardsInfo[id].image;

                AddNewPlayerCard(_meta.data.playerCards.playerCards[i], imgPath);
            }
        }

        public void refreshUpgrList()
        {
            upgradingdcards.Clear();
            Debug.Log($"upgradingdcards.Count: {upgradingdcards.Count}");
        }
        public void upgradeCard(int UID)    //UPGRADE
        {
            //не добавляем по второму разу
            if (upgradingdcards.Contains(UID))
            {    
                return;
            }
            if (upgradingdcards.Count > 0) 
            {
                int BaseCardGID = _meta.data.playerCards[upgradingdcards[0]].cardId;
                //не добавляем карту с отличным GID
                if (_meta.data.playerCards[UID].cardId != BaseCardGID) { return; }
            }
            upgradingdcards.Add(UID);
            
            PlayerCard baseCard = _meta.data.playerCards[UID];
            int currentLevel = baseCard.level;
            if ( currentLevel <= _meta.data.playerCards.minCount4LvlUp.Length - 1 )
            {
                int Count4Upgrade = _meta.data.playerCards.minCount4LvlUp[baseCard.level];
                int currentCardCount = upgradingdcards.Count;

                if (currentCardCount >= Count4Upgrade)
                {
                    PlayerCard pc = _meta.data.playerCards.addCardToPlayer( baseCard.cardId, currentLevel + 1 );
                    _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].addCard( pc.UID );
                    //удаляем карты которые выбирались для апгрейда
                    foreach (int ups_uid in upgradingdcards)
                    {
                        _meta.data.playerCards.playerCards.RemoveAll( x => x.UID == ups_uid );
                    }
                    refreshUpgrList();
                    
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
        {    //почистить список карт в колоде (view)
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

        public void addCardToDeck(int uid, int deckID)
        {
            PlayerCard addedCard = _meta.data.playerCards[uid];
            if (addedCard == null)
            {
                Debug.Log("public void addCardToDeck( int cardId, int deckId): card with this cardId not Exist");
                return;
            }

            CardInfo cardInfo = _meta.data.allCardsInfo[addedCard.cardId];
            if (_meta.data.playerCards.playerDecks[deckID] == null)
            {
                Debug.Log("public void addCardToDeck( int cardId, int deckId): deck with this deckId not Exist");
                return;
            }
            if ( !(cardInfo.deckType == _meta.data.playerCards.playerDecks[deckID].deckType || 
                   _meta.data.playerCards.playerDecks[deckID].deckType == DeckType.Common))
            {
                Debug.Log($"cardInfo.deckType != playerDecks[deckId].deckType");
                return;
            }

            if (_meta.data.playerCards.playerDecks[ _meta.data.playerCards.activeDeckID ].cards.Exists( x => x == uid))
            {
                Debug.Log($"Такая карта уже есть (uid:{uid})");
                return;
            }
            
            _meta.data.playerCards.playerDecks[deckID].addCard(addedCard.UID);
            Debug.Log($"added card lId: {addedCard.UID} to deck {deckID}");
            spawnPlayerCardsInDeck();
        }

        public void removeCardFromDeck(int uid, int deckID)
        {
            if (_meta.data.playerCards.playerCards.Count-1<uid && 
                _meta.data.playerCards.playerDecks.Count-1<deckID)
            {
                return;
            }
            _meta.data.playerCards.playerDecks[deckID].cards.Remove(uid);
            spawnPlayerCardsInDeck();
        }

        public void removeCardFromCurrentDeck(int uid)
        {
            removeCardFromDeck(uid, _meta.data.playerCards.activeDeckID);
        }
        
        public void addCardToCurrentDeck(int uid)
        {
            addCardToDeck(uid, _meta.data.playerCards.activeDeckID);
        }

        public void nextDeck()
        {
            int deckID = ++ _meta.data.playerCards.activeDeckID;
            deckID %= _meta.data.playerCards.playerDecks.Count;
            loadDeck(deckID);
        }
    }
}