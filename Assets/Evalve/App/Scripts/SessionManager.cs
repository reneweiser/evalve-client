using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evalve.Client;
using Evalve.Contracts;

namespace Evalve.App
{
    public class SessionManager : ISessionManager
    {
        private readonly IConnection _connection;
        private readonly ILogger _logger;
        private readonly Session _session;

        public string SelectedTeamId => _session.UserSelectedTeam;
        public IReadOnlyList<string> SelectedAssetIds => _session.UserSelectedAssets;
        public IReadOnlyList<string> SelectedObjectIds => _session.UserSelectedObjects;
        
        public SessionManager(IConnection connection, ILogger logger, Session session)
        {
            _connection = connection;
            _logger = logger;
            _session = session;
        }
        
        public async Task Login(string email, string password)
        {
            _session.UserProfile = await _connection.Login(email, password);
            _connection.SetAuthToken(_session.UserProfile.Token);
            _session.UserTeams = (await _connection.GetTeamsAsync()).ToDictionary(t => t.Id, t => t);
            _logger.Logged += AddLog;
        }

        public async Task Logout()
        {
            await _connection.Logout();
            _logger.Logged -= AddLog;
        }

        public async Task PullTeams()
        {
            var teams = await _connection.GetTeamsAsync();
            
            if (teams.Count == 0)
                throw new Exception("No teams found");
                
            _session.UserTeams = teams.ToDictionary(t => t.Id, t => t);
            _session.UserSelectedTeam = teams.First().Id;
        }

        public async Task PullAssets()
        {
            var assetBundles = await _connection.GetAssetBundlesAsync();
            _session.UserAssets = assetBundles.ToDictionary(i => i.Id, i => i);
        }

        public async Task PullObjects()
        {
            var objects = await _connection.GetSceneObjectsAsync();
            _session.UserSceneObjects = objects.ToDictionary(i => i.Id, i => i);
        }

        public async Task PushObjects()
        {
            var operations = _session.UserSceneObjects.Values
                .Where(i => i.IsDirty)
                .Select(i => _connection.UpdateSceneObjectAsync(i.Id, i));
            await Task.WhenAll(operations);
        }

        public void SelectTeam(string id)
        {
            if (_session == null)
                throw new Exception("User must be logged in");
            
            _session.UserSelectedTeam = id;
        }

        public Team GetTeam(string id)
        {
            if (!_session.UserTeams.TryGetValue(id, out var team))
                throw new Exception("Team not found");

            return team;
        }

        public void SelectAssets(List<string> assetIds)
        {
            _session.UserSelectedAssets = assetIds;
        }

        public void SelectObjects(List<string> objectIds)
        {
            _session.UserSelectedObjects = objectIds;
        }

        public SceneObject GetObject(string objectId)
        {
            if (!_session.UserSceneObjects.TryGetValue(objectId, out var sceneObject))
                throw new Exception("Scene object not found");
                
            return sceneObject;
        }

        public void UpdateObject(string objectId, SceneObjectBehaviour sceneObject)
        {
            if (!_session.UserSceneObjects.TryGetValue(objectId, out var old))
            {
                old = new SceneObject
                {
                    Id = objectId,
                    TeamId = _session.UserSelectedTeam,
                };
            }
            
            _session.UserSceneObjects[objectId] = sceneObject.ToData(old);
        }

        private void AddLog(string message)
        {
            _session.Log += "\n" + message;
        }
    }
}