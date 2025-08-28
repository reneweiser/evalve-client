using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evalve.Client
{
    public class LocalConnection : IConnection
    {
        private string _authToken;

        public void SetAuthToken(string authToken)
        {
            _authToken = authToken;
        }

        public Task<UserProfile> Login(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse> Logout()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Team>> GetTeamsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<SceneObject>> GetSceneObjectsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<SceneObject> GetSceneObjectAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<SceneObject> CreateSceneObjectAsync(SceneObject sceneObject)
        {
            throw new System.NotImplementedException();
        }

        public Task<SceneObject> UpdateSceneObjectAsync(string id, SceneObject sceneObject)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteSceneObjectAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<AssetBundle> GetAssetBundleAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<AssetBundle>> GetAssetBundlesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task DownloadAssetBundleFileAsync(AssetBundle assetBundle)
        {
            throw new System.NotImplementedException();
        }
    }
}