using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class UTController : Controller
    {    //Upgrade Tree Controller
        public TextAsset JSON;
        private UpgradeTree tree;
        public override void Init(Meta meta)
        {
            tree = new UpgradeTree();
            //tree = parseJSON(JSON.text);
        }

        public UpgradeTree parseJSON( string JSONstr)
        {
            return JsonUtility.FromJson<UpgradeTree>(JSONstr);
        }
        
        void Update()
        {
        
        }
    }
}
