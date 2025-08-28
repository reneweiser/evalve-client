using Newtonsoft.Json;

namespace Evalve.Client
{
    public class UserProfile
    {
        [JsonProperty("token")]
        public string Token;
        [JsonProperty("email")]
        public string Email;
    }
}