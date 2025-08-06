using Newtonsoft.Json;

namespace Evalve.Client
{
    public class AssetBundle
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("team_id")]
        public string TeamId;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("crc")]
        public uint Checksum;
        [JsonProperty("unity_version")]
        public string UnityVersion;
        [JsonProperty("url")]
        public string Url;
        [JsonProperty("assets")]
        public Asset[] Elements;
    }

    public class Asset
    {
        [JsonProperty("name")]
        public string Name;
    }
}