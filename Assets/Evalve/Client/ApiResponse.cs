using Newtonsoft.Json;

namespace Evalve.Client
{
    public class ApiResponse
    {
        [JsonProperty("message")]
        public string Message;
    }
}