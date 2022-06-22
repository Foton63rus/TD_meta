using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence
{
    public class PlayerCardController : Controller
    {
        public UnityAction<int, string> OnPlayerCardDrawNewOne;
        public UnityAction OnPlayerCardsClearAll;
    
        private Meta meta;
        [SerializeField]
        private PlayerCardView View;    // Контейнер UI для карт
        [SerializeField]
        private GameObject PlayerCardPrefab;

        public override void Init( Meta meta)
        {
            this.meta = meta;
            this.meta.OnAllCardInfoLoad += spawnPlayerCards;    //подписка спавна карт на загрузку всех карт из JSON
            View.Init( this, PlayerCardPrefab );    //Инициализация вьюхи
        }

        void spawnPlayerCards()
        {
            int cardsCount = meta.playerCards.playerDecks[meta.activeDeck].cards.Count;

            ClearPlayerCards();
        
            for (int i = 0; i < cardsCount; i++)
            {
                int localCardID = meta.playerCards.playerDecks[meta.activeDeck].cards[i];
                PlayerCard playerCard = meta.playerCards.playerCards[localCardID];
                int globalCardID = playerCard.cardId;
                string imgPath = meta.allCardsInfo[globalCardID].image;

                AddNewCard(globalCardID, imgPath);
            }
        }

        private void AddNewCard(int cardID, string imageSource)
        {
            OnPlayerCardDrawNewOne?.Invoke(cardID, imageSource);
        }
        private void ClearPlayerCards()
        {    //почистить список карт игрока
            OnPlayerCardsClearAll?.Invoke();
        }
    }
}