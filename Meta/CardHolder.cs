using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class CardHolder : MonoBehaviour
    {
        public TextAsset asset;
        public AllCardsInfo allCards;
        void Start()
        {
            allCards = JsonUtility.FromJson<AllCardsInfo>(asset.text);
            Debug.Log( $"{allCards.cards[0].image}" );
        }
    }
}

