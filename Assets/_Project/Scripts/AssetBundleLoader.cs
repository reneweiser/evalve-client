using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Evalve
{
    public class AssetBundleLoader
    {
        public static event Action<GameObject> Loaded;
        public static async Task LoadAssetsAsync(string bundlePath, string[] assetNames)
        {
            var bundle = await LoadAssetBundleAsync(bundlePath);
    
            if (bundle == null)
            {
                Logger.Log("Failed to load AssetBundle!");
                return;
            }

            await Task.WhenAll(assetNames
                .OrderBy(assetName => assetName)
                .Select(assetName => LoadAssetFromBundleAsync(bundle, assetName)));

            bundle.Unload(false);
        }

        private static async Task<AssetBundle> LoadAssetBundleAsync(string url)
        {
            var bundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, url));
    
            while (!bundleRequest.isDone)
            {
                await Task.Yield();
            }

            if (bundleRequest.assetBundle != null)
                return bundleRequest.assetBundle;
            
            Logger.Log($"Failed to load AssetBundle from {url}");
            return null;
        }

        private static async Task LoadAssetFromBundleAsync(AssetBundle bundle, string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                Logger.Log("Asset name is empty!");
                return;
            }

            var assetRequest = bundle.LoadAssetAsync<GameObject>(assetName);
    
            while (!assetRequest.isDone)
            {
                await Task.Yield();
            }

            if (assetRequest.asset == null)
            {
                Logger.Log($"Failed to load asset {assetName} from AssetBundle");
                return;
            }

            var loadedAsset = assetRequest.asset as GameObject;
            Loaded?.Invoke(Object.Instantiate(loadedAsset));
            Logger.Log($"Successfully loaded and instantiated {assetName}");
        } 
    }
}