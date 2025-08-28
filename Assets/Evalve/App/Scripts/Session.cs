using System.Collections.Generic;
using Evalve.Client;
using AssetBundle = Evalve.Client.AssetBundle;

namespace Evalve.App
{
    public class Session
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool UserRememberMe { get; set; }
        public UserProfile UserProfile { get; set; }
        public string Log { get; set; }
        
        public string UserSelectedTeam { get; set; }
        public Dictionary<string, Team> UserTeams { get; set; }
        
        public List<string> UserSelectedAssets { get; set; }
        public Dictionary<string, AssetBundle> UserAssets { get; set; }
        
        public List<string> UserSelectedObjects { get; set; }
        public Dictionary<string, SceneObject> UserSceneObjects { get; set; }
        public Dictionary<string, SceneObjectBehaviour> UserSceneObjectBehaviours { get; set; }
    }
}