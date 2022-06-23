using System;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class DataContainer : MonoBehaviour
    {
        private Meta meta;
        public Data goDataHolder;
        public void Init(Meta meta)
        {
            this.meta = meta;
            goDataHolder.Init(meta);
        }
    }
}
