using System;
using System.Collections.Generic;

namespace TowerDefence
{
    public class MetaEvents { }
    
    
    //EVENT ARGS
    
    //При событии очистки карт игрока (отрисовка)
    public struct OnPlayerCardsClearAll{}

    //При событии добавления карты (отрисовка)
    public struct OnPlayerCardDrawNewOne
    {
        public int cardID;
        public string imageSource;

        public OnPlayerCardDrawNewOne(int cardID, string imageSource)
        {
            this.cardID      = cardID;
            this.imageSource = imageSource;
        }
    }

}
