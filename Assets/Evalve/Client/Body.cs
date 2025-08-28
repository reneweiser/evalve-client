using Newtonsoft.Json;

namespace Evalve.Client
{
    public class Body : Property
    {
        [JsonProperty("position")]
        public Vector Position;
        [JsonProperty("rotation")]
        public Vector Rotation;

        public override object ToDynamic()
        {
            return new{};
        }
    }
}