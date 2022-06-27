using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class ShopView : MonoBehaviour, IReceive<OnShopSlotAddNew>
    {
        private ShopController _controller;
        private GameObject _shopSlotPrefab;
        public void Init( ShopController controller, GameObject shopSlotPrefab )
        {
            _controller = controller;
            _shopSlotPrefab = shopSlotPrefab;
            
            EventController.Add(this);
        }

        public void AddNewShopSlot( in OnShopSlotAddNew arg )
        {
            GameObject newSlot = Instantiate(_shopSlotPrefab, transform);
            ShopSlotMono mono = newSlot.GetComponent<ShopSlotMono>();

            mono.goHeader.GetComponent<TextMeshProUGUI>().text = arg.deckType.ToString();
            
            if (arg.visible == SlotType.Hidden)
            {
                newSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>(arg.imgPath);
            }
            else
            {
                newSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Spell_002");
                //newSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>(arg.imgPath);
            }

            if ( arg.currency == Currency.GameMoney)
            {
                mono.goFooterPrice.GetComponent<TextMeshProUGUI>().text = arg.price.ToString();
                mono.goCurrency.GetComponent<Image>().sprite = Resources.Load<Sprite>("brilliant");
            } else if (arg.currency == Currency.RealMoney)
            {
                mono.goFooterPrice.GetComponent<TextMeshProUGUI>().text = arg.price.ToString();
                mono.goCurrency.GetComponent<Image>().sprite = Resources.Load<Sprite>("coins");
            }
            else if(arg.currency == Currency.Ads)
            {
                mono.goFooterPrice.GetComponent<TextMeshProUGUI>().text = "";
                mono.goCurrency.GetComponent<Image>().sprite = Resources.Load<Sprite>("ads");
            }
            else
            {
                mono.goFooterPrice.GetComponent<TextMeshProUGUI>().text = "FREE";
                mono.goCurrency.SetActive(false);
            }
            //mono.goCurrency.GetComponent<Image>().sprite
        }

        public void HandleSignal(in OnShopSlotAddNew arg)
        {
            AddNewShopSlot(arg);
        }
    }
}

