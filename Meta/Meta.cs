using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

            fakeCurrencyLoad();
        }

        private void InitControllers()
        {    //Инициализация Контроллеров
            controllers = GetComponent<ControllerContainer>();
            controllers.Init(this);
        }

        private void fakeCurrencyLoad()
        {
            data.gameCurrency.Add(1000);    //Currency.Free
            data.gameCurrency.Add(300);     //Currency.Ads
            data.gameCurrency.Add(499);   //Currency.GameMoney
            data.gameCurrency.Add(70000); //Currency.RealMoney

            /*data.gameCurrency.Add(new GameCurrency( Currency.Free, 10));
            data.gameCurrency.Add(new GameCurrency( Currency.Ads, 3));
            data.gameCurrency.Add(new GameCurrency( Currency.GameMoney, 499));
            data.gameCurrency.Add(new GameCurrency( Currency.RealMoney, 70000));*/
        }

        private void Update()
        {
            
        }
    }
}