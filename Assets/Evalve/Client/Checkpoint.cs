using Newtonsoft.Json;

namespace Evalve.Client
{
    public class Checkpoint : Property
    {
        [JsonProperty("perimeter")]
        public float Perimeter;

        public override object ToDynamic()
        {
            return new{};
        }
    }
}