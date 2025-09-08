using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Evalve.App
{
    public class AssetBundleLoader
    {
        private readonly IAssetManager _assetManager;
        private readonly Contracts.ILogger _logger;

        public AssetBundleLoader(IAssetManager assetManager, Contracts.ILogger logger)
        {
            _assetManager = assetManager;
            _logger = logger;
        }
        
        public async Task LoadAssetsAsync(string bundlePath, string[] assetNames)
        {
            var bundle = await LoadAssetBundleAsync(bundlePath);
    
            if (bundle == null)
            {
                _logger.Log("Failed to load AssetBundle!", Contracts.LogType.Error);
                return;
            }

            await Task.WhenAll(assetNames
                .OrderBy(assetName => assetName)
                .Select(assetName => LoadAssetFromBundleAsync(bundle, assetName)));

            bundle.Unload(false);
        }

        private async Task<AssetBundle> LoadAssetBundleAsync(string url)
        {
            var bundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, url));
    
            while (!bundleRequest.isDone)
            {
                await Task.Yield();
            }

            if (bundleRequest.assetBundle != null)
                return bundleRequest.assetBundle;
            
            _logger.Log($"Failed to load AssetBundle from {url}", Contracts.LogType.Error);
            return null;
        }

        private async Task LoadAssetFromBundleAsync(AssetBundle bundle, string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                _logger.Log("Asset name is empty!", Contracts.LogType.Error);
                return;
            }

            var assetRequest = bundle.LoadAssetAsync<GameObject>(assetName);
    
            while (!assetRequest.isDone)
            {
                await Task.Yield();
            }

            if (assetRequest.asset == null)
            {
                _logger.Log($"Failed to load asset {assetName} from AssetBundle", Contracts.LogType.Error);
                return;
            }

            var loadedAsset = assetRequest.asset as GameObject;
            var obj = Object.Instantiate(loadedAsset);
            _assetManager.Register(obj.name, obj);
            _logger.Log($"Instantiated {assetName}", Contracts.LogType.Success);
        } 
    }
}