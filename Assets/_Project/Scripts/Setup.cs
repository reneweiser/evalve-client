using System;
using System.Linq;
using System.Threading.Tasks;
using Evalve.Client;
using UnityEngine;

namespace Evalve
{
    public class Setup : MonoBehaviour
    {
        [SerializeField] private SceneObjectFactory _sceneObjectFactory;

        private async void Start()
        {
            const string url = "http://localhost/api/v1";

            var connection = new Connection(url);
            
            var assetBundle = await connection.GetAssetBundleAsync("01k1e7bm3kx6gdrb80m346dc2x");
            var assetNames = assetBundle.Elements
                .Select(element => element.Name)
                .Where(elementName => elementName.Contains("Option 03"))
                .ToArray();
            
            await FileDownloader.DownloadFileAsync(assetBundle.Url, assetBundle.Id);
            await AssetBundleLoader.LoadAssetsAsync(assetBundle.Id, assetNames);
        }

        private async Task SpawnSceneObjects(Connection connection)
        {
            var sceneObjects = await connection.GetSceneObjectsAsync();

            foreach (var sceneObject in sceneObjects)
            {
                _sceneObjectFactory.Create(sceneObject);
            }
        }
    }
}
