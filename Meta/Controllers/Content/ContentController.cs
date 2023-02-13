using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TowerDefence
{
    public class ContentController : Controller
    {
        private GameObject _cachedObject;
        public override void Init(Meta meta)
        {
            //BuildTarget.StandaloneWindows64
        }

        private async void Load(string assetId)
        {
            AsyncOperationHandle handle = Addressables.LoadAssetAsync<GameObject>(assetId);
            await handle.Task;
            
            if (handle.Result != null)
            {
                MetaEvents.OnAssetLoaded.Invoke( new LoadedAssetGameObject()
                {
                    AddressableName = assetId, Asset = (GameObject) handle.Result
                } );
            }
        }

        private async void Instantiate( string assetId )
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(assetId);
            await handle.Task;
            
            // if (handle.Result != null)
            // {
            //     MetaEvents.OnAssetLoaded.Invoke( new LoadedAssetGameObject()
            //     {
            //         AddressableName = assetId, Asset = handle.Result
            //     } );
            // }
        }
    }
}
