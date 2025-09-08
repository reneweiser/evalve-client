using Newtonsoft.Json;

namespace Evalve.Client
{
    public class ApiResponse
    {
        [JsonProperty("message")]
        public string Message;
    }

    public class UploadSceneObjectThumbnailSuccessResponse
    {
        [JsonProperty("path")]
        public string Path;
        [JsonProperty("message")]
        public string Message;
        [JsonProperty("url")]
        public string Url;
    }
}