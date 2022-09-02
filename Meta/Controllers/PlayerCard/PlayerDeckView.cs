using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class PlayerDeckView : MonoBehaviour
    {
        public Button nextDeckBtn;
        public Button addCardToCurrentDeckBtn;
        public Button removeCardFromCurrentDeckBtn;
        public Button upgradeCardBtn;
        
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
            
            nextDeckBtn.onClick.AddListener(                 () => { OnBtnNextDeck();});
            addCardToCurrentDeckBtn.onClick.AddListener(     () => { OnBtnAddCardToCurrentDeck();});
            removeCardFromCurrentDeckBtn.onClick.AddListener(() => { OnBtnRemoveCardFromCurrentDeck(); });
            upgradeCardBtn.onClick.AddListener(              () => { OnBtnUpgradeCard(); });
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
                Debug.Log($"card:{playerCard.cardId} lvl:{playerCard.level} uid:{playerCard.UID}");
                //MetaEvents.OnRemoveCardFromDeck.Invoke(playerCard.localId);
                _commandConfigurator.Execute(playerCard.UID);
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
            _commandConfigurator.Execute();
        }

        public void OnBtnAddCardToCurrentDeck()
        {
            _commandConfigurator.switchCommand(new COMAddCardToCurrentDeck(_controller));
        }

        public void OnBtnRemoveCardFromCurrentDeck()
        {
            _commandConfigurator.switchCommand(new COMRemoveCardFromCurrentDeck(_controller));
        }

        public void OnBtnUpgradeCard()
        {
            _controller.refreshUpgrList();
            _commandConfigurator.switchCommand( new COMUpgradeCard(_controller));
        }
    }
}