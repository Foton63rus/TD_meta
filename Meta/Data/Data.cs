using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Data : MonoBehaviour
    {
        public string jsonUrl = "game.aivanof.ru/api/";
        public AllCardsInfo allCardsInfo;
        public PlayerCards playerCards;
        public Shop shop;
        //public List<GameCurrency> gameCurrency;
        public List<int> gameCurrency = new List<int>();
        
        public void Init(Meta meta)
        {
            meta.data = this;
        }
    }
}
