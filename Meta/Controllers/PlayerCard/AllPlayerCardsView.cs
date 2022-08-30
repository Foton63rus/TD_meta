using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class AllPlayerCardsView : MonoBehaviour
    {
        private Transform _transform;
        private PlayerCardCommandConfigurator _commandConfigurator;
        private PlayerCardController _controller;
        private GameObject _cardPrefab;
         
        public void Init( PlayerCardController controller, 
            PlayerCardCommandConfigurator commandConfigurator,
            GameObject cardPrefab)
        {
            _transform = transform;
            _commandConfigurator = commandConfigurator;
            _controller = controller;
            _cardPrefab = cardPrefab;
            
            MetaEvents.OnPlayerCardDrawNewOne  += OnAllCardSpawnOneHandler;
            MetaEvents.OnPlayerCardClearView += clear;
        }

        public void clear()
        {
            for (int i = _transform.childCount-1; i >= 0 ; i--)
            {
                Destroy(_transform.GetChild(i).gameObject);
            }
        }

        public void AddNewCard( PlayerCard playerCard, string imgPath )
        {
            //создаем карту
            GameObject newCard = Instantiate(_cardPrefab, _transform);
            //назначаем картинку для карты
            newCard.GetComponent<Image>().sprite = Resources.Load<Sprite>(imgPath);
            //сохраняем в карте значение её ID
            newCard.GetComponent<PlayerCardMono>().globalCardID = playerCard.cardId;
            //добавляем действия на клик
            newCard.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log($"card:{playerCard}");
                _commandConfigurator.Execute(new []{playerCard.localId});
            } );
        }

        public void OnAllCardSpawnOneHandler(OnPlayerCardDrawNewOneEventArgs arg)
        {
            AddNewCard(arg.playerCard, arg.imageSource);
        }
    }
}
