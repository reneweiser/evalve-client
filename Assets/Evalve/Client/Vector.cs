using Newtonsoft.Json;

namespace Evalve.Client
{
    public class Vector
    {
        [JsonProperty("x")]
        public float X;
        [JsonProperty("y")]
        public float Y;
        [JsonProperty("z")]
        public float Z;
    }
}