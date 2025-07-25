using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Evalve
{
    public class AssetBundleLoader : MonoBehaviour
    {
        public async Task LoadAssetsAsync(string bundlePath, string[] assetNames)
        {
            var bundle = await LoadAssetBundleAsync(bundlePath);
    
            if (bundle == null)
            {
                Debug.LogError("Failed to load AssetBundle!");
                return;
            }

            await Task.WhenAll(assetNames.Select(assetName => LoadAssetFromBundleAsync(bundle, assetName)));

            bundle.Unload(false);
        }

        private async Task<AssetBundle> LoadAssetBundleAsync(string url)
        {
            var bundleRequest = AssetBundle.LoadFromFileAsync(url);
    
            while (!bundleRequest.isDone)
            {
                await Task.Yield();
            }

            if (bundleRequest.assetBundle != null)
                return bundleRequest.assetBundle;
            
            Debug.LogError($"Failed to load AssetBundle from {url}");
            return null;
        }

        private async Task LoadAssetFromBundleAsync(AssetBundle bundle, string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                Debug.LogError("Asset name is empty!");
                return;
            }

            var assetRequest = bundle.LoadAssetAsync<GameObject>(assetName);
    
            while (!assetRequest.isDone)
            {
                await Task.Yield();
            }

            if (assetRequest.asset == null)
            {
                Debug.LogError($"Failed to load asset {assetName} from AssetBundle");
                return;
            }

            var loadedAsset = assetRequest.asset as GameObject;
            Instantiate(loadedAsset);
            Debug.Log($"Successfully loaded and instantiated {assetName}");
        } 
        
        private async void Start()
        {
            var names = new[]
            {
                "Assets/MLH/MLH_E - Urban 01 Planting Red.dae",
                "Assets/MLH/MLH_E - Scen 01 Furniture F1+2.dae",
                "Assets/MLH/MLH_E - Arch 01 no Doors.dae",
                "Assets/MLH/MLH_E - Scen 01 Entrouage.dae",
                "Assets/MLH/MLH_E - Scen 01 Furniture F3.dae",
                "Assets/MLH/MLH_E - Urban 01 Entrouage.dae",
                "Assets/MLH/MLH_E - Scen 01 Planting.dae",
                "Assets/MLH/MLH_E - Urban 01.dae",
                "Assets/MLH/MLH_E - Urban 01 Planting.dae",
                "Assets/MLH/MLH_E - Arch 01 no Topo.dae",
                "Assets/MLH/MLH_E - Scen 01 Furniture Roof.dae",
                "Assets/MLH/MLH_E - Arch 01 Door open.dae",
                "Assets/MLH/MLH_E - Urban 01 TopoBuild.dae",
                "Assets/MLH/MLH_E - Arch 01 Door Flat closed.dae",
                "Assets/MLH/MLH_E - Arch 01.dae",
                "Assets/MLH/MLH_E - Urban 01 Billboard.dae",
            };
            
            var path = Path.Combine(Application.streamingAssetsPath, "mlh");
            
            await LoadAssetsAsync(path, names);
        }
    }
}