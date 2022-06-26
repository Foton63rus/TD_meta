using System;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class DataContainer : MonoBehaviour
    {
        private Meta meta;
        public Data goDataHolder;    //Gameobject на котором весит компонент Data ( он и хранит всю дату)
        public void Init(Meta meta)
        {
            this.meta = meta;
            goDataHolder.Init(meta);
        }
    }
}
