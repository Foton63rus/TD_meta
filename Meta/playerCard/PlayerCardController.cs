using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCardController : MonoBehaviour
{
    public UnityAction<int, string> OnPlayerCardDrawNewOne;
    public UnityAction OnPlayerCardsClearAll;
    
    private Meta Meta;
    public PlayerCardView View;    // Контейнер UI для карт
    public GameObject PlayerCardPrefab;

    public void Init()
    {
        Meta = gameObject.GetComponent<Meta>();
        Meta.OnAllCardInfoLoad += spawnPlayerCards;    //подписка спавна карт на загрузку всех карт из JSON
        View.Init( this, PlayerCardPrefab );    //Инициализация вьюхи
    }

    void spawnPlayerCards()
    {
        int cardsCount = Meta.playerCards.playerDecks[Meta.activeDeck].cards.Count;

        ClearPlayerCards();
        
        for (int i = 0; i < cardsCount; i++)
        {
            int localCardID = Meta.playerCards.playerDecks[Meta.activeDeck].cards[i];
            PlayerCard playerCard = Meta.playerCards.playerCards[localCardID];
            int globalCardID = playerCard.cardId;
            string imgPath = Meta.allCardsInfo[globalCardID].image;

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
