using UnityEngine;

namespace TowerDefence
{
    public class JSONLoaderController : Controller
    {
        private Meta meta;
        
        public TextAsset allCardsInfoAsset;
        public TextAsset playerCardsAsset;

        public override void Init(Meta meta)
        {
            this.meta = meta;

            LoadAllJSONs();
        }
        
        private void LoadAllJSONs()
        {// Загрузка всех JSON
            allCardInfoLoad();
            playerCardsLoad();
            
            meta.onAllCardInfoLoad?.Invoke();
        }
        
        private void allCardInfoLoad()
        {// Загрузка JSON с описанием всех карт
            meta.data.allCardsInfo = JsonUtility.FromJson<AllCardsInfo>(allCardsInfoAsset.text);
        }
    
        private void playerCardsLoad()
        { // Загрузка JSON с описанием карт игрока
            meta.data.playerCards = JsonUtility.FromJson<PlayerCards>(playerCardsAsset.text);
        }
    }
}
