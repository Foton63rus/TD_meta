using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence
{
    public static class MetaEvents
    {

        // Meta Events
        public static UnityAction OnMetaLoaded;                        //Когда мета полностью загрузилась
        
        public static UnityAction<LoadedAssetGameObject> OnAssetLoaded;
        
        // WEB REQUESTES
        public static UnityAction<string> WebGetRequest;
        public static UnityAction<string, string[]> WebGetRequestWithArgs;
        public static UnityAction<string, string> OnWebResponse; //Ответ с сервера
        
        //JSON
        //public static UnityAction<string> OnServerJsonRequest;        //Запрос необходимого json по адресу (постфиксу)
        public static UnityAction<string, string> OnServerJsonResponse; //Ответ с сервера

        //Shop events
        public static UnityAction<int> BannerClicked;
        
        //Achievemnts events
        public static UnityAction OnAchievement;
        public static UnityAction OnAchievementTestAction;

        //Cards Events
        public static UnityAction OnPlayerDeckClearAll; //Событие очистки карт в колоде игрока (отрисовка)
        public static UnityAction<OnPlayerCardDrawNewOneEventArgs> OnPlayerCardDrawNewOne; // Собятие для отрисовки всех карт в вьюхе
        public static UnityAction<OnPlayerCardDrawNewOneEventArgs> OnPlayerCardInDeckDrawNewOne; //Событие добавления карты для вьюхи (отрисовка)
        public static UnityAction OnPlayerCardClearView; //Событие очистки карт игрока (отрисовка)

        //Game Events
        
    }
    
    //EVENT ARGS
    public struct LoadedAssetGameObject
    {
        public string AddressableName;
        public GameObject Asset;
    }
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
