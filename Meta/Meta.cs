using System;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class Meta : MonoBehaviour
    {    // Мета игры
        public UpgradeTree ugradetree = new UpgradeTree();

        private DataContainer metaData;
        private ControllerContainer controllers;

        public Data data;

        public void Start()
        {
            InitMetaData();
            InitControllers();
        }

        private void InitMetaData()
        {    //Инициализация Даты
            metaData = GetComponent<DataContainer>();
            metaData.Init(this);
        }

        private void InitControllers()
        {    //Инициализация Контроллеров
            controllers = GetComponent<ControllerContainer>();
            controllers.Init(this);
        }

        private void Update()
        {
            
        }
    }
}