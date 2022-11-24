using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class Meta : MonoBehaviour
    {    // Мета игры
        private DataContainer metaData;
        public ControllerContainer controllers;
        public Data data;
        public IWebController Web;

        public void Start()
        {
            InitMetaData();
            Web = GetComponent<WebController>().Initialize(this);
            
            InitControllers();
            MetaEvents.OnMetaLoaded?.Invoke();
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
    }
}