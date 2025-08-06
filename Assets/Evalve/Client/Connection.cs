using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Evalve.Client
{
    public class Connection
    {
        private readonly string _baseUrl;

        public Connection(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        
        public async Task<List<SceneObject>> GetSceneObjectsAsync()
        {
            var uri = $"{_baseUrl}/scene-objects";
            using var request = UnityWebRequest.Get(uri);
            
            var operation = request.SendWebRequest();
            
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                throw new System.Exception($"HTTP Error: {request.error} ({uri})");
            }

            var jsonResponse = request.downloadHandler.text;
            
            return JsonConvert.DeserializeObject<List<SceneObject>>(jsonResponse);
        }

        public async Task<SceneObject> GetSceneObjectAsync(string id)
        {
            var uri = $"{_baseUrl}/scene-objects/{id}";
            using var request = UnityWebRequest.Get(uri);
            
            var operation = request.SendWebRequest();
            
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                throw new System.Exception($"HTTP Error: {request.error} ({uri})");
            }

            var jsonResponse = request.downloadHandler.text;
            
            return JsonConvert.DeserializeObject<SceneObject>(jsonResponse);
        }

        public async Task<AssetBundle> GetAssetBundleAsync(string id)
        {
            var uri = $"{_baseUrl}/asset-bundles/{id}";
            using var request = UnityWebRequest.Get(uri);
            
            var operation = request.SendWebRequest();
            
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                throw new System.Exception($"HTTP Error: {request.error} ({uri})");
            }

            var jsonResponse = request.downloadHandler.text;
            
            return JsonConvert.DeserializeObject<AssetBundle>(jsonResponse);
        }

        public async Task<List<AssetBundle>> GetAssetBundlesAsync()
        {
            var uri = $"{_baseUrl}/asset-bundles";
            using var request = UnityWebRequest.Get(uri);
            
            var operation = request.SendWebRequest();
            
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                throw new System.Exception($"HTTP Error: {request.error} ({uri})");
            }

            var jsonResponse = request.downloadHandler.text;
            
            return JsonConvert.DeserializeObject<List<AssetBundle>>(jsonResponse);
        }
    }
}
