using Newtonsoft.Json;

namespace Evalve.Client
{
    public class Transform
    {
        [JsonProperty("position")]
        public Vector Position;
        [JsonProperty("rotation")]
        public Vector Rotation;
    }
}