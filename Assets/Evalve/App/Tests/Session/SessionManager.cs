using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evalve.Client;

namespace Evalve.App.Tests.Session
{
    public class SessionManager
    {
        public App.Session Session { get; private set; }

        private readonly IConnection _connection;

        public SessionManager(IConnection connection, App.Session session)
        {
            _connection = connection;
            Session = session;
        }

        public async Task Login(string email, string password)
        {
            Session.UserProfile = await _connection.Login(email, password);
            _connection.SetAuthToken(Session.UserProfile.Token);
            Session.UserTeams = (await _connection.GetTeamsAsync()).ToDictionary(t => t.Id, t => t);
        }

        public async Task Logout()
        {
            await _connection.Logout();
            Session = null;
        }

        public void SelectTeam(string teamId)
        {
            if (Session == null)
                throw new Exception("User must be logged in");
            
            Session.UserSelectedTeam = teamId;
        }

        public async Task PullUserData()
        {
            if (Session == null)
                throw new Exception("User must be logged in");
            if (Session.UserSelectedTeam == null)
                throw new Exception("User must select a team");

            Session.UserAssets = (await _connection.GetAssetBundlesAsync())
                .ToDictionary(item => item.Id, item => item);
            
            Session.UserSceneObjects = (await _connection.GetSceneObjectsAsync())
                .ToDictionary(item => item.Id, item => item);
        }

        public void SelectAssets(List<string> assetIds)
        {
            Session.UserSelectedAssets = assetIds;
        }

        public void SelectObjects(List<string> objects)
        {
            Session.UserSelectedObjects = objects;
        }

        public void AddLogEntry(string message)
        {
            Session.Log += "\n" + message;
        }

        public void SetSceneObjectBehaviours(Dictionary<string,SceneObjectBehaviour> behaviours)
        {
            Session.UserSceneObjectBehaviours = behaviours;
        }

        public void RenameObject(string sceneObjectId, string newName)
        {
            Session.UserSceneObjects[sceneObjectId].Name = newName;
            Session.UserSceneObjects[sceneObjectId].IsDirty = true;
        }
    }
}