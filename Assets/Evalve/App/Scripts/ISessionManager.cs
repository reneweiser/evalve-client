using System.Collections.Generic;
using System.Threading.Tasks;
using Evalve.Client;

namespace Evalve.App
{
    public interface ISessionManager
    {
        string SelectedTeamId { get; }
        IReadOnlyList<string> SelectedAssetIds { get; }
        IReadOnlyList<string> SelectedObjectIds { get; }
        Task Login(string email, string password);
        Task Logout();
        Task PullTeams();
        Task PullAssets();
        Task PullObjects();
        Task PushObjects();
        void SelectTeam(string id);
        Team GetTeam(string id);
        void SelectAssets(List<string> assetIds);
        void SelectObjects(List<string> objectIds);
        SceneObject GetObject(string objectId);
        void UpdateObject(string objectId, SceneObjectBehaviour sceneObject);
    }
}