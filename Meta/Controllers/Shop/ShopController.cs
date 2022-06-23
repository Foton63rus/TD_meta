using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class ShopController : Controller
    {
        private Meta _meta;
        [SerializeField] private ShopView _view;

        public Shop shop;

        public override void Init(Meta meta)
        {
            _meta = meta;
            _view.Init(this);
        }
    }
}