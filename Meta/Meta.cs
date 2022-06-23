using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace TowerDefence
{
    [Serializable]
    public class Meta : MonoBehaviour
    {    // Мета игры
        public DataContainer metaData;
        public ControllerContainer controllers;

        // Событие, возникающее после загрузки JSON со всеми картами
        public UnityAction onAllCardInfoLoad;

        public Data data;

        public void Start()
        {
            InitMetaData();
            InitControllers();
        }

        private void InitMetaData()
        {
            metaData = GetComponent<DataContainer>();
            metaData.Init(this);
        }

        private void InitControllers()
        {
            controllers = GetComponent<ControllerContainer>();
            controllers.Init(this);
        }
    }
}