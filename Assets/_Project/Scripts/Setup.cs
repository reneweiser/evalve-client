using System.Linq;
using System.Threading.Tasks;
using Evalve.Client;
using Evalve.SceneObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Evalve
{
    public class Setup : MonoBehaviour
    {
        [FormerlySerializedAs("_sceneObjectFactory")] [SerializeField] private Factory factory;
        [SerializeField] private string _assetId;
        [SerializeField] private string _nameFilter;

        private async void Start()
        {
            const string url = "http://localhost/api/v1";

            var connection = new Connection(url);
            
            var assetBundle = await connection.GetAssetBundleAsync(_assetId);
            var assetNames = assetBundle.Elements
                .Select(element => element.Name)
                .Where(elementName => elementName.Contains(_nameFilter))
                .ToArray();
            
            await FileDownloader.DownloadFileAsync(assetBundle.Url, assetBundle.Id);
            await AssetBundleLoader.LoadAssetsAsync(assetBundle.Id, assetNames);
        }

        private async Task SpawnSceneObjects(Connection connection)
        {
            var sceneObjects = await connection.GetSceneObjectsAsync();

            foreach (var sceneObject in sceneObjects)
            {
                factory.Create(sceneObject);
            }
        }
    }
}
