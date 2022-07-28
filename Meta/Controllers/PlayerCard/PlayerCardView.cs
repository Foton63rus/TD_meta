using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class PlayerCardView : MonoBehaviour
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
            
            MetaEvents.OnPlayerCardsClearAll  += OnPlayerCardsClearAllHandler;
            MetaEvents.OnPlayerCardDrawNewOne  += OnPlayerCardDrawNewOneHandler;
            
            nextDeckBtn.onClick.AddListener(() => { OnNextDeck();});
        }

        public void AddNewCard( int globalCardID, string imgPath )
        {
            //создаем карту
            GameObject newCard = Instantiate(_cardPrefab, _transform);
            //назначаем картинку для карты
            newCard.GetComponent<Image>().sprite = Resources.Load<Sprite>(imgPath);
            //сохраняем в карте значение её ID
            newCard.GetComponent<PlayerCardMono>().globalCardID = globalCardID;
            //добавляем действия на клик
            newCard.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log($"card:{globalCardID}");
            } );
        }

        public void OnPlayerCardDrawNewOneHandler(OnPlayerCardDrawNewOneEventArgs arg)
        {
            AddNewCard(arg.cardID, arg.imageSource);
        }

        public void OnPlayerCardsClearAllHandler()
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