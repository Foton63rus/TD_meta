using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence
{
    public static class MetaEvents
    {
        // Meta Events
        public static UnityAction OnMetaLoaded;                        //Когда мета полностью загрузилась
        
        //JSON
        public static UnityAction<string> OnServerJsonRequest;        //Запрос необходимого json по адресу (постфиксу)
        public static UnityAction<string, string> OnServerJsonResponse; //Ответ с сервера

        // UI MENU Events
        public static UnityAction OnMenuUISettingsClick;    //клик кнопки настроек
        public static UnityAction OnMenuUIMessagesClick;    //клик кнопки сообщений
        public static UnityAction OnMenuUIAchievementsClick;    //клик кнопки достижений
        public static UnityAction OnMenuUIDeckChoiceClick;    //клик кнопки выбора колоды
        public static UnityAction OnMenuUIMapChoiceClick;    //клик кнопки выбора карты
        
        //Shop evens
        
        //Achievemnts events
        public static UnityAction OnAchievement;
        public static UnityAction OnAchievementTestAction;

        //Cards Events
        public static UnityAction OnPlayerDeckClearAll; //Событие очистки карт в колоде игрока (отрисовка)
        public static UnityAction<OnPlayerCardDrawNewOneEventArgs> OnPlayerCardDrawNewOne; // Собятие для отрисовки всех карт в вьюхе
        public static UnityAction<OnPlayerCardDrawNewOneEventArgs> OnPlayerCardInDeckDrawNewOne; //Событие добавления карты для вьюхи (отрисовка)
        public static UnityAction OnPlayerCardClearView; //Событие очистки карт игрока (отрисовка)
        public static UnityAction<CardInfo> OnPlayerCardAdd; //для контроллера (покупка ии просто добавлние)
        public static UnityAction<int> OnRemoveCardFromDeck; //Удалить карту из колоды

        //Game Events
        
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
    
    public enum EventType
    {
        MetaLoaded = 0
    }
    
}
