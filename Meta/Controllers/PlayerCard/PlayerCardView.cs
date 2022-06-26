using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class PlayerCardView : MonoBehaviour, 
        IReceive<OnPlayerCardsClearAll>, IReceive<OnPlayerCardDrawNewOne>
    {   
        private Transform _transform;
        private PlayerCardController _controller;
        private GameObject _cardPrefab;

        public void Init( PlayerCardController controller, GameObject cardPrefab)
        {
            _transform  = transform;
            _controller = controller;
            _cardPrefab = cardPrefab;

            EventController.Add(this);
            //_controller.OnPlayerCardsClearAll  += ClearPreviousCards;
            //_controller.OnPlayerCardDrawNewOne += AddNewCard;
        }

        public void AddNewCard( int globalCardID, string imgPath )
        {
            //создаем карту
            GameObject newCard = Instantiate(_cardPrefab, Vector3.zero, Quaternion.identity, _transform);
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

        public void HandleSignal(in OnPlayerCardDrawNewOne arg)
        {
            AddNewCard(arg.cardID, arg.imageSource);
        }

        public void HandleSignal(in OnPlayerCardsClearAll arg)
        {
            ClearPreviousCards();
        }

        public void ClearPreviousCards()
        {
            for (int i = _transform.childCount-1; i >= 0 ; i--)
            {
                Destroy(_transform.GetChild(i).gameObject);
            }
        }
    }
}