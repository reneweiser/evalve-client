using Newtonsoft.Json;

namespace Evalve.Client
{
    public class SceneObject
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("team_id")]
        public string TeamId;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("transform")]
        public Transform Transform;
        [JsonProperty("properties")]
        public Property[] Properties;
        
        [JsonIgnore]
        public bool IsSelected;
        [JsonIgnore]
        public bool IsDirty;

        public void Update(SceneObject sceneObject)
        {
            if (sceneObject.Name != null)
                Name = sceneObject.Name;
            if (sceneObject.Transform != null)
                Transform = sceneObject.Transform;
            if (sceneObject.Properties != null)
                Properties = sceneObject.Properties;
            
            IsSelected = sceneObject.IsSelected;
            IsDirty = true;
        }
    }
}