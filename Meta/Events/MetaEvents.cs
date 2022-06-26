using System.Collections.Generic;

namespace TowerDefence
{
    public class MetaEvents { }
    
    
    //EVENT ARGS
    public struct OnPlayerCardsClearAll{}

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

    public struct GetObjFromJSON<T>
    {
        
    }
}
