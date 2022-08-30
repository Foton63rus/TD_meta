using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class PlayerDeckView : MonoBehaviour
    {
        public Button nextDeckBtn;
        public Button addCardToCurrentDeck;
        public Button removeCardFromCurrentDeck;
        
        private Transform _transform;
        private PlayerCardController _controller;
        private PlayerCardCommandConfigurator _commandConfigurator;
        private GameObject _cardPrefab;
        
        public void Init( PlayerCardController controller, PlayerCardCommandConfigurator commandConfigurator, GameObject cardPrefab)
        {
            _transform  = transform;
            _controller = controller;
            _commandConfigurator = commandConfigurator;
            _cardPrefab = cardPrefab;
            
            MetaEvents.OnPlayerDeckClearAll  += OnPlayerDeckClearAllHandler;
            MetaEvents.OnPlayerCardInDeckDrawNewOne  += OnPlayerCardDrawNewOneHandler;
            
            nextDeckBtn.onClick.AddListener(() => { OnBtnNextDeck();});
            addCardToCurrentDeck.onClick.AddListener(() => { OnBtnAddCardToCurrentDeck();});
            removeCardFromCurrentDeck.onClick.AddListener(() => { OnBtnRemoveCardFromCurrentDeck(); });
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
                //MetaEvents.OnRemoveCardFromDeck.Invoke(playerCard.localId);
                _commandConfigurator.Execute(new []{playerCard.localId});
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

        public void OnBtnNextDeck()
        {
            _commandConfigurator.addCommand(new COMNextDeck(_controller));
            _commandConfigurator.Execute(null);
        }

        public void OnBtnAddCardToCurrentDeck()
        {
            _commandConfigurator.switchCommand(new COMAddCardToCurrentDeck(_controller));
        }

        public void OnBtnRemoveCardFromCurrentDeck()
        {
            _commandConfigurator.switchCommand(new COMRemoveCardFromCurrentDeck(_controller));
        }
    }
}