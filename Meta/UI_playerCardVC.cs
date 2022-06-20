using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_playerCardVC : MonoBehaviour
{
    private Meta Meta;
    public GameObject Content;
    public GameObject ImagePrefab;

    public void Start()
    {
        Meta = gameObject.GetComponent<Meta>();
        Meta.OnAllCardInfoLoad += Init;
    }

    void Init()
    {
        var cardsCount = Meta.playerCards.playerDecks[Meta.activeDeck].cards.Count;
        Debug.Log($"Init: aDeck:{Meta.activeDeck}, count({cardsCount})");

        ClearPreviousContent();
        for (int i = 0; i < cardsCount; i++)
        {
            GameObject go = GameObject.Instantiate(ImagePrefab, Vector3.zero, Quaternion.identity, Content.transform);
            int cID = Meta.playerCards.playerDecks[Meta.activeDeck].cards[i];
            PlayerCard playerCard = Meta.playerCards.playerCards[cID];
            int gcID = playerCard.cardId;
            string imgPath = Meta.allCardsInfo[gcID].image;
            
            go.GetComponent<PlayerCardBtn>().Init(gcID, imgPath);
            
        }
    }

    void ClearPreviousContent()
    {
        for (int i = Content.transform.childCount-1; i >= 0 ; i--)
        {
            Destroy(Content.transform.GetChild(i).gameObject);
        }
    }
}
