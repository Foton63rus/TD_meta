using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_playerCardVC : MonoBehaviour
{
    private Meta Meta;
    public GameObject PlayerCardUIContainer;    // Контейнер UI для карт
    public GameObject PlayerCardPrefab; 

    public void Init()
    {
        Meta = gameObject.GetComponent<Meta>();
        Meta.OnAllCardInfoLoad += spawnPlayerCards;    //подписка спавна карт на загрузку всех карт из JSON
    }

    void spawnPlayerCards()
    {
        var cardsCount = Meta.playerCards.playerDecks[Meta.activeDeck].cards.Count;
        Debug.Log($"Init: aDeck:{Meta.activeDeck}, count({cardsCount})");

        ClearPreviousContent();
        for (int i = 0; i < cardsCount; i++)
        {
            GameObject go = GameObject.Instantiate(PlayerCardPrefab, Vector3.zero, Quaternion.identity, PlayerCardUIContainer.transform);
            int localCardID = Meta.playerCards.playerDecks[Meta.activeDeck].cards[i];
            PlayerCard playerCard = Meta.playerCards.playerCards[localCardID];
            int globalCardID = playerCard.cardId;
            string imgPath = Meta.allCardsInfo[globalCardID].image;
            
            go.GetComponent<PlayerCardBtn>().Init(globalCardID, imgPath);
            
        }
    }

    void ClearPreviousContent()
    {
        for (int i = PlayerCardUIContainer.transform.childCount-1; i >= 0 ; i--)
        {
            Destroy(PlayerCardUIContainer.transform.GetChild(i).gameObject);
        }
    }
}
