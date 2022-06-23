using UnityEngine;

namespace TowerDefence
{
    public class Data : MonoBehaviour
    {
        public AllCardsInfo allCardsInfo;
        public PlayerCards playerCards;

        public void Init(Meta meta)
        {
            meta.data = this;
        }
    }
}
