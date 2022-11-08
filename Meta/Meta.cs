using System;
using UnityEngine;
using UnityEngine.Localization.Tables;

namespace TowerDefence
{
    [Serializable]
    public class Meta : MonoBehaviour
    {    // Мета игры
        private DataContainer metaData;
        private ControllerContainer controllers;

        public Data data;

        public void Start()
        {
            InitMetaData();
            InitControllers();

            MetaEvents.OnMetaLoaded?.Invoke();
            Debug.Log( $"gameCurrency {JsonUtility.ToJson( data.gameCurrency )}" );
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