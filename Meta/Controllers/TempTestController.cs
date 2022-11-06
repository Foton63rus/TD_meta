using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class TempTestController : Controller
    {
        private Meta meta;
        public override void Init(Meta meta)
        {
            this.meta = meta;
            fakeCurrencyLoad(meta);
        }
        
        private void fakeCurrencyLoad(Meta meta)
        {
            meta.data.gameCurrency.Add(1000);    //Currency.Free
            meta.data.gameCurrency.Add(300);     //Currency.Ads
            meta.data.gameCurrency.Add(499);   //Currency.GameMoney
            meta.data.gameCurrency.Add(70000); //Currency.RealMoney
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }
    }
}
