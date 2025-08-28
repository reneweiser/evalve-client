using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using ILogger = Evalve.Contracts.ILogger;
using LogType = Evalve.Contracts.LogType;

namespace Evalve.Client
{
    public class Connection : IConnection
    {
        private readonly string _baseUrl;
        private readonly ILogger _logger;
        private string _authToken;

        public Connection(string baseUrl, ILogger logger)
        {
            _baseUrl = baseUrl;
            _logger = logger;
        }

        public void SetAuthToken(string authToken)
        {
            _authToken = authToken;
        }

        public async Task<UserProfile> Login(string email, string password)
        {
            var uri = $"{_baseUrl}/login";

            using var client = new HttpClient();
            var body = JsonConvert.SerializeObject(new { email = email, password = password });
            var response = await client.PostAsync(uri, new StringContent(body, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            
            var userProfile = JsonConvert.DeserializeObject<UserProfile>(content);
            _logger.Log($"Successfully logged in with {userProfile.Email}!", LogType.Success);

            return userProfile;
        }

        public async Task<ApiResponse> Logout()
        {
            var uri = $"{_baseUrl}/logout";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            
            var response = await client.PostAsync(uri, new StringContent(""));
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            _logger.Log($"Successfully logged out!", LogType.Success);
            
            return JsonConvert.DeserializeObject<ApiResponse>(content);
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            var uri = $"{_baseUrl}/v1/teams";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Team>>(content);
        }
        
        public async Task<List<SceneObject>> GetSceneObjectsAsync()
        {
            var uri = $"{_baseUrl}/v1/scene-objects";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<SceneObject>>(content);
        }

        public async Task<SceneObject> GetSceneObjectAsync(string id)
        {
            var uri = $"{_baseUrl}/v1/scene-objects/{id}";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SceneObject>(content);
        }

        public async Task<SceneObject> CreateSceneObjectAsync(SceneObject sceneObject)
        {
            var uri = $"{_baseUrl}/v1/scene-objects";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var body = JsonConvert.SerializeObject(sceneObject, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            });
            var response = await client.PostAsync(uri, new StringContent(body));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SceneObject>(content);
        }

        public async Task<SceneObject> UpdateSceneObjectAsync(string id, SceneObject sceneObject)
        {
            var uri = $"{_baseUrl}/v1/scene-objects/{id}";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var body = JsonConvert.SerializeObject(sceneObject, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            });
            var response = await client.PutAsync(uri, new StringContent(body));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SceneObject>(content);
        }

        public async Task DeleteSceneObjectAsync(string id)
        {
            var uri = $"{_baseUrl}/v1/scene-objects/{id}";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);
            
            var response = await client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task<AssetBundle> GetAssetBundleAsync(string id)
        {
            var uri = $"{_baseUrl}/v1/asset-bundles/{id}";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AssetBundle>(content);
        }

        public async Task<List<AssetBundle>> GetAssetBundlesAsync()
        {
            var uri = $"{_baseUrl}/v1/asset-bundles";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<AssetBundle>>(content);
        }

        public async Task DownloadAssetBundleFileAsync(AssetBundle assetBundle)
        {
            var filePath = Path.Combine(Application.streamingAssetsPath, assetBundle.Id);
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);
            using var response = await client.GetAsync(assetBundle.Url, HttpCompletionOption.ResponseHeadersRead);
            
            response.EnsureSuccessStatusCode();

            await using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                         fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, useAsync: true))
            {
                await contentStream.CopyToAsync(fileStream);
            }
            
            _logger.Log($"File downloaded successfully to: {filePath}", LogType.Success);
        }
    }
}
