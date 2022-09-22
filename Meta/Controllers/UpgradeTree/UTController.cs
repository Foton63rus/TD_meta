using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class UTController : Controller
    {    //Upgrade Tree Controller
        public TextAsset JSON;
        public UpgradeTree tree;
        public override void Init(Meta meta)
        {
            tree = new UpgradeTree();
            tree = parseJSON(JSON.text);

            testfunc();
        }

        public UpgradeTree parseJSON( string JSONstr)
        {
            return JsonUtility.FromJson<UpgradeTree>(JSONstr);
        }

        private void testfunc()
        {
            tree.OpenNode( tree[0].Name );
            tree.OpenNode( tree[1].Name );
            tree.OpenNode( tree[2].Name );
            tree.OpenNode( tree[3].Name );
        }
        
        void Update()
        {
        
        }
    }
}
