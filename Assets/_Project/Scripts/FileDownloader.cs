using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Evalve
{
    public class FileDownloader
    {
        public static async Task DownloadFileAsync(string url, string fileName)
        {
            try
            {
                var filePath = Path.Combine(Application.streamingAssetsPath, fileName);

                using var webRequest = UnityWebRequest.Get(url);
                await webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Download failed: {webRequest.error}");
                    return;
                }

                var fileData = webRequest.downloadHandler.data;

                await File.WriteAllBytesAsync(filePath, fileData);
                
                Debug.Log($"File successfully downloaded and saved to: {filePath}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error downloading file: {ex.Message}");
            }
        }
    }
}