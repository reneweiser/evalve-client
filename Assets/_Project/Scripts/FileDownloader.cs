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
                    Logger.Log($"Download failed: {webRequest.error}");
                    return;
                }

                var fileData = webRequest.downloadHandler.data;

                await File.WriteAllBytesAsync(filePath, fileData);
                
                Logger.Log($"File successfully downloaded and saved to: {filePath}");
            }
            catch (System.Exception ex)
            {
                Logger.Log($"Error downloading file: {ex.Message}");
            }
        }
    }
}