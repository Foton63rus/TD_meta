using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class ShopView : MonoBehaviour
    {
        private ShopController _controller;
        private GameObject _shopSlotPrefab;
        public void Init( ShopController controller, GameObject shopSlotPrefab )
        {
            _controller = controller;
            _shopSlotPrefab = shopSlotPrefab;
        }
    }
}

