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
            testUTreeCreate();
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
        }

        private void testUTreeCreate()
        {
            UTNode node = new UTNode();
            node.Name = "utn_towerforce_01";
            node.Description = "some force for tower";
            node.Cost = 5;
            
            UTNode node2 = new UTNode();
            node2.Name = "utn_unitforce_01";
            node2.Description = "some force for unit";
            node2.Cost = 20;
            
            ugradetree.AddPoints(50);
            ugradetree.AddNode( node );
            ugradetree.AddNode( node2 );
            node.SetChild(node2);
            //node.IsOpen = true;
            
            string JSON = UTLoader.Save(ugradetree);
            UpgradeTree tree2 = UTLoader.Load(JSON);
            Debug.Log( JSON );
            ugradetree.OpenNode("utn_unitforce_01");
            Debug.Log( ugradetree["utn_unitforce_01"] );
        }

        private void Update()
        {
            
        }
    }
}