using System;
using UnityEngine;

namespace TowerDefence
{
    public class PlayerCardController : Controller
    {
        private Meta _meta;
        [SerializeField] private PlayerCardView view;    // Контейнер UI для карт в текущей колоде
        [SerializeField] private GameObject playerCardPrefab;
        [SerializeField] private TextAsset allCardsInfoAsset;
        [SerializeField] private TextAsset playerCardsAsset;

        public override void Init( Meta meta)
        {
            _meta = meta;
            
            meta.data.allCardsInfo = JsonUtility.FromJson<AllCardsInfo>(allCardsInfoAsset.text);
            meta.data.playerCards = JsonUtility.FromJson<PlayerCards>(playerCardsAsset.text);

            MetaEvents.OnPlayerCardAdd += addNewCard;
            
            view.Init( this, playerCardPrefab );    //Инициализация вьюхи
            
            ClearPlayerCards();
            spawnPlayerCards();
        }

        void spawnPlayerCards()
        {
            int cardsCount = _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards.Count;

            for (int i = 0; i < cardsCount; i++)
            {
                int localCardID = _meta.data.playerCards.playerDecks[_meta.data.playerCards.activeDeckID].cards[i];
                PlayerCard playerCard = _meta.data.playerCards.playerCards[localCardID];
                int globalCardID = playerCard.cardId;
                string imgPath = _meta.data.allCardsInfo[globalCardID].image;

                AddNewCardToView(globalCardID, imgPath);
            }
        }

        private void addNewCard( CardInfo cardInfo )
        {
            //находим карту в открытых картах у игрока с заданным id и уровнем.
            PlayerCard ExistPlayerCard = _meta.data.playerCards.playerCards.Find( x=> x.cardId == cardInfo.id);
            if ( ExistPlayerCard == null)
            {
                Debug.Log(_meta.data.playerCards.playerCards.Count);
                _meta.data.playerCards.addCardToPlayer(cardInfo.id);
                Debug.Log(_meta.data.playerCards.playerCards.Count);
            }
            else
            {
                ExistPlayerCard.count++;
                Debug.Log($"card with id: {ExistPlayerCard.cardId} ,count up to 1 = {ExistPlayerCard.count}");
            }
        }

        private void AddNewCardToView(int cardID, string imageSource)
        {    //Добавить карту
            MetaEvents.OnPlayerCardDrawNewOne?.Invoke(new OnPlayerCardDrawNewOneEventArgs(cardID, imageSource));
        }
        private void ClearPlayerCards()
        {    //почистить список карт игрока
            MetaEvents.OnPlayerCardsClearAll?.Invoke();
        }

        public void loadDeck(int deckID)
        {
            if (deckID >= 0 && deckID < _meta.data.playerCards.playerDecks.Count)
            {
                _meta.data.playerCards.activeDeckID = deckID;
            }
            ClearPlayerCards();
            spawnPlayerCards();
        }
        
        public void nextDeck()
        {
            int deckID = ++ _meta.data.playerCards.activeDeckID;
            deckID %= _meta.data.playerCards.playerDecks.Count;
            loadDeck(deckID);
        }
    }
}