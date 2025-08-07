using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Evalve.Client
{
    public class Connection
    {
        private readonly string _baseUrl;
        private readonly string _authToken;

        public Connection(string baseUrl, string authToken)
        {
            _baseUrl = baseUrl;
            _authToken = authToken;
        }
        
        public async Task<List<SceneObject>> GetSceneObjectsAsync()
        {
            var uri = $"{_baseUrl}/scene-objects";
            using var request = UnityWebRequest.Get(uri);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _authToken);
            
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
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _authToken);
            
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

        public async Task<SceneObject> CreateSceneObjectAsync(SceneObject sceneObject)
        {
            var uri = $"{_baseUrl}/scene-objects";

            Debug.Log(JsonConvert.SerializeObject(sceneObject, Formatting.Indented));
            using var request = UnityWebRequest.Post(uri, JsonConvert.SerializeObject(sceneObject, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            }), "application/json");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _authToken);
            
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
            
            Debug.Log(jsonResponse);
            return JsonConvert.DeserializeObject<SceneObject>(jsonResponse);
        }

        public async Task UpdateSceneObjectAsync(string id, SceneObject sceneObject)
        {
            var uri = $"{_baseUrl}/scene-objects/{id}";

            using var request = UnityWebRequest.Put(uri, JsonConvert.SerializeObject(sceneObject, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            }));
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _authToken);
            
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
            
            Debug.Log(jsonResponse);
        }

        public async Task DeleteSceneObjectAsync(string id)
        {
            var uri = $"{_baseUrl}/scene-objects/{id}";

            using var request = UnityWebRequest.Delete(uri);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _authToken);
            
            var operation = request.SendWebRequest();
            
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            
            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                throw new System.Exception($"HTTP Error: {request.error} ({uri})");
            }

            // var jsonResponse = request.downloadHandler.text;
            
            // Debug.Log(jsonResponse);
        }

        public async Task<AssetBundle> GetAssetBundleAsync(string id)
        {
            var uri = $"{_baseUrl}/asset-bundles/{id}";
            using var request = UnityWebRequest.Get(uri);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _authToken);
            
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
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _authToken);
            
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
