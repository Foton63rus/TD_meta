using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Data : MonoBehaviour
    {
        public AllCardsInfo allCardsInfo;
        public PlayerCards playerCards;
        public Shop shop;
        public List<GameCurrency> gameCurrency;
        
        public void Init(Meta meta)
        {
            meta.data = this;
            gameCurrency = new List<GameCurrency>();
        }
    }
}
