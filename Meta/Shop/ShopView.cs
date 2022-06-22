using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class ShopView : MonoBehaviour
    {
        private ShopController _controller;
        public void Init( ShopController controller )
        {
            _controller = controller;
        }
    }
}

