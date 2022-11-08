using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class UTController : Controller
    {    //Upgrade Tree Controller
        private string address;
        public UpgradeTree tree;
        public IUpgradeTree UTInterface;
        public override void Init(Meta meta)
        {
            address = "upgrade_tree";
            MetaEvents.OnServerJsonResponse += OnServerJsonResponse;
            MetaEvents.OnServerJsonRequest.Invoke( address );
        }

        public void OnServerJsonResponse(string addr, string resp)
        {
            if (address == addr)
            {
                tree = JsonUtility.FromJson<UpgradeTree>(resp);
            }
        }
    }
}
