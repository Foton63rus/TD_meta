using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence
{
    public static class MetaEvents
    {
        public static UnityAction OnPlayerCardsClearAll; //Событие очистки карт игрока (отрисовка)
        public static UnityAction<OnPlayerCardDrawNewOneEventArgs> OnPlayerCardDrawNewOne; //Событие добавления карты (отрисовка)
        public static UnityAction<CardInfo> OnPlayerCardAdd;
    }
    
    //EVENT ARGS
    [SerializeField] public struct OnPlayerCardDrawNewOneEventArgs
    {
        public int cardID;
        public string imageSource;

        public OnPlayerCardDrawNewOneEventArgs(int cardID, string imageSource)
        {
            this.cardID      = cardID;
            this.imageSource = imageSource;
        }
    }

}
