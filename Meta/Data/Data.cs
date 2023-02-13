using UnityEngine;

namespace TowerDefence
{
    public class Data : MonoBehaviour
    {
        //public string url = "game.aivanof.ru/api/";
        public string token = "testtoken_19519jfsh2if22f2f";
        public AllCardsInfo allCardsInfo;
        public PlayerCards playerCards;
        public Shop shop;
        public Route route;

        public void Init(Meta meta)
        {
            route = new Route();
            meta.data = this;
        }
    }
}
