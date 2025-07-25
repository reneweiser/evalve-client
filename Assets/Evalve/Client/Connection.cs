using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Evalve.Api
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
            using var request = UnityWebRequest.Get($"{_baseUrl}/scene-objects");
            
            var operation = request.SendWebRequest();
            
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                throw new System.Exception($"HTTP Error: {request.error}");
            }

            var jsonResponse = request.downloadHandler.text;
            
            return JsonConvert.DeserializeObject<List<SceneObject>>(jsonResponse);
        }
    }
}
