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
            
            UTNode node2 = new UTNode();
            node2.Name = "utn_unitforce_01";
            node2.Description = "some force for unit";

            ugradetree.AddNode( node );
            ugradetree.AddNode( node2 );
            node.SetChild(node2);
            node.IsOpen = true;
            
            UTLoader loader = new UTLoader();
            string JSON = loader.Save(ugradetree);
            UpgradeTree tree2 = loader.Load(JSON);
            Debug.Log( "tree2" );
            Debug.Log( tree2 );
        }

        private void Update()
        {
            
        }
    }
}