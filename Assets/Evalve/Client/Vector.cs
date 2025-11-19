using Newtonsoft.Json;

namespace Evalve.Client
{
    public class Resolution
    {
        [JsonProperty("width")]
        public float Width;
        [JsonProperty("height")]
        public float Height;
    }
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