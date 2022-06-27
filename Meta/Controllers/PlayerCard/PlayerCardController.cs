using System;
using UnityEngine;

namespace TowerDefence
{
    public class PlayerCardController : Controller
    {
        //public UnityAction<int, string> OnPlayerCardDrawNewOne;
        //public UnityAction OnPlayerCardsClearAll;

        private Meta meta;
        [SerializeField] private PlayerCardView View;    // Контейнер UI для карт
        [SerializeField] private GameObject PlayerCardPrefab;
        [SerializeField] private TextAsset allCardsInfoAsset;
        [SerializeField] private TextAsset playerCardsAsset;

        public override void Init( Meta meta)
        {
            this.meta = meta;
            
            meta.data.allCardsInfo = JsonUtility.FromJson<AllCardsInfo>(allCardsInfoAsset.text);
            meta.data.playerCards = JsonUtility.FromJson<PlayerCards>(playerCardsAsset.text);
            
            View.Init( this, PlayerCardPrefab );    //Инициализация вьюхи

            spawnPlayerCards();
        }

        void spawnPlayerCards()
        {
            int cardsCount = meta.data.playerCards.playerDecks[meta.data.playerCards.activeDeck].cards.Count;
            ClearPlayerCards();
        
            for (int i = 0; i < cardsCount; i++)
            {
                int localCardID = meta.data.playerCards.playerDecks[meta.data.playerCards.activeDeck].cards[i];
                PlayerCard playerCard = meta.data.playerCards.playerCards[localCardID];
                int globalCardID = playerCard.cardId;
                string imgPath = meta.data.allCardsInfo[globalCardID].image;

                AddNewCard(globalCardID, imgPath);
            }
        }

        private void AddNewCard(int cardID, string imageSource)
        {    //Добавить карту
            //OnPlayerCardDrawNewOne?.Invoke(cardID, imageSource);
            EventController.Invoke(new OnPlayerCardDrawNewOne(cardID, imageSource));
        }
        private void ClearPlayerCards()
        {    //почистить список карт игрока
            //OnPlayerCardsClearAll?.Invoke();
            EventController.Invoke( new OnPlayerCardsClearAll());
        }
    }
}