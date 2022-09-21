using UnityEngine;

namespace TowerDefence
{
    public static class UTLoader
    {
        public static string Save( UpgradeTree tree )
        {
            return JsonUtility.ToJson(tree);
        }

        public static UpgradeTree Load( string JSONstr)
        {
            return JsonUtility.FromJson<UpgradeTree>(JSONstr);
        }
    }
}
