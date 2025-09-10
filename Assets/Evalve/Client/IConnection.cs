using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evalve.Client
{
    public interface IConnection
    {
        void SetAuthToken(string authToken);
        Task<UserProfile> Login(string email, string password);
        Task<ApiResponse> Logout();
        Task<List<Team>> GetTeamsAsync();
        Task<List<SceneObject>> GetSceneObjectsByTeamAsync(string teamId);
        Task<SceneObject> GetSceneObjectAsync(string id);
        Task<SceneObject> CreateSceneObjectAsync(SceneObject sceneObject);
        Task<SceneObject> UpdateSceneObjectAsync(string id, SceneObject sceneObject);
        Task<string> UploadSceneObjectThumbnail(string id, byte[] thumbnail);
        Task DeleteSceneObjectAsync(string id);
        Task<AssetBundle> GetAssetBundleAsync(string id);
        Task<List<AssetBundle>> GetAssetBundlesByTeamAsync(string teamId);
        Task DownloadAssetBundleFileAsync(AssetBundle assetBundle);
    }
}