using System.Collections.Generic;
using System.Linq;

namespace Evalve.App.States.CreatingSessions
{
    public class SelectingResourcesModel : Model
    {
        private readonly Session _session;

        public SelectingResourcesModel(Session session)
        {
            _session = session;
        }
        
        public string SelectedTeam
        {
            get => _session.UserSelectedTeam;
            set => _session.UserSelectedTeam = value;
        }

        public List<string> SelectedAssets
        {
            get => _session.UserSelectedAssets;
            set => _session.UserSelectedAssets = value;
        }

        public List<string> SelectedObjects
        {
            get => _session.UserSelectedObjects;
            set => _session.UserSelectedObjects = value;
        }

        public Dictionary<string, string> Teams => _session.UserTeams.ToDictionary(i => i.Key, i => i.Value.Name);
        public Dictionary<string, string> Assets => _session.UserAssets.ToDictionary(i => i.Key, i => i.Value.Name);
        public Dictionary<string, string> Objects => _session.UserSceneObjects.ToDictionary(i => i.Key, i => i.Value.Name);
    }
}