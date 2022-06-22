using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling.Memory.Experimental;
using Random = UnityEngine.Random;

namespace TowerDefence
{
    [Serializable]
    public class Meta : MonoBehaviour
    {    // Мета игры

        [SerializeField]
        public MetaData data;

        public ControllerContainer controllers;
        
        // Событие, возникающее после загрузки JSON со всеми картами
        public UnityAction OnAllCardInfoLoad;
        
        private PlayerCardController playerCardController;
        private ShopController shopController;
        
        public int stars;
        public int crystal;
        
        // Ссылка на JSON с описанием всех карт
        public TextAsset allCardsInfoAsset;
        public AllCardsInfo allCardsInfo;
    
        // Ссылка на JSON с картами игрока
        public TextAsset playerCardsAsset;
        public PlayerCards playerCards;
    
        // индекс активной колоды игрока
        public int activeDeck;
    
        public void Start()
        {
            controllers = GetComponent<ControllerContainer>();
            controllers.Init(this);

            LoadAllJSONs();
        }
    
        public void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.O))
            {
                string text = JsonUtility.ToJson(playerCards);
                Debug.Log(text);
            }*/
        }
    
        private void LoadAllJSONs()
        {// Загрузка всех JSON
            allCardInfoLoad();
            playerCardsLoad();
            
            OnAllCardInfoLoad?.Invoke();
        }
    
        private void allCardInfoLoad()
        {// Загрузка JSON с описанием всех карт
            allCardsInfo = JsonUtility.FromJson<AllCardsInfo>(allCardsInfoAsset.text);
        }
    
        private void playerCardsLoad()
        { // Загрузка JSON с описанием карт игрока
            playerCards = JsonUtility.FromJson<PlayerCards>(playerCardsAsset.text);
        }
    }
}




