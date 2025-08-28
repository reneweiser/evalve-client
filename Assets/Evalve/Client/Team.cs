using Newtonsoft.Json;

namespace Evalve.Client
{
    public class Team
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("name")]
        public string Name;
    }
}