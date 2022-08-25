using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class PlayerDeckView : MonoBehaviour
    {
        public Button nextDeckBtn;
        
        private Transform _transform;
        private PlayerCardController _controller;
        private GameObject _cardPrefab;
        
        public void Init( PlayerCardController controller, GameObject cardPrefab)
        {
            _transform  = transform;
            _controller = controller;
            _cardPrefab = cardPrefab;
            
            MetaEvents.OnPlayerDeckClearAll  += OnPlayerDeckClearAllHandler;
            MetaEvents.OnPlayerCardInDeckDrawNewOne  += OnPlayerCardDrawNewOneHandler;
            
            nextDeckBtn.onClick.AddListener(() => { OnNextDeck();});
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
                Debug.Log($"card:{playerCard.cardId}");
                MetaEvents.OnRemoveCardFromDeck.Invoke(playerCard.localId);
            } );
        }

        public void OnPlayerCardDrawNewOneHandler(OnPlayerCardDrawNewOneEventArgs arg)
        {
            AddNewCard(arg.playerCard, arg.imageSource);
        }

        public void OnPlayerDeckClearAllHandler()
        {
            for (int i = _transform.childCount-1; i >= 0 ; i--)
            {
                Destroy(_transform.GetChild(i).gameObject);
            }
        }

        public void OnNextDeck()
        {
            _controller.nextDeck();
        }
    }
}