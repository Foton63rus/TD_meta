using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence
{
    public static class MetaEvents
    {
        public static UnityAction OnPlayerDeckClearAll; //Событие очистки карт игрока (отрисовка)
        public static UnityAction<OnPlayerCardDrawNewOneEventArgs> OnPlayerCardDrawNewOne; // Собятие для отрисовки всех карт в вьюхе
        public static UnityAction<OnPlayerCardDrawNewOneEventArgs> OnPlayerCardInDeckDrawNewOne; //Событие добавления карты для вьюхи (отрисовка)
        public static UnityAction OnPlayerCardClearView;
        public static UnityAction<CardInfo> OnPlayerCardAdd; //для контроллера (покупка ии просто добавлние)
        public static UnityAction<int> OnTryAddCardToCurrentDeck;
    }
    
    //EVENT ARGS
    [SerializeField] public struct OnPlayerCardDrawNewOneEventArgs
    {
        public PlayerCard playerCard;
        public string imageSource;

        public OnPlayerCardDrawNewOneEventArgs(PlayerCard playerCard, string imageSource)
        {
            this.playerCard  = playerCard;
            this.imageSource = imageSource;
        }
    }

}
