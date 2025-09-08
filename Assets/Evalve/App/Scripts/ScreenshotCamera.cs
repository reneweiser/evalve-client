using System;
using System.IO;
using UnityEngine;

namespace Evalve.App
{
    public class ScreenshotCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private const int _width = 1920;
        private const int _height = 1080;

        public void CreateScreenshot(string filename, Action<string> callback)
        {
            gameObject.SetActive(true);
            
            // Create a RenderTexture with the desired resolution
            var renderTexture = new RenderTexture(_width, _height, 24);
            _camera.targetTexture = renderTexture;

            // Render the camera's view to the RenderTexture
            _camera.Render();

            // Activate the RenderTexture to read pixels
            RenderTexture.active = renderTexture;
            var screenshot = new Texture2D(_width, _height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);
            screenshot.Apply();

            // Save the screenshot as a PNG
            var bytes = screenshot.EncodeToPNG();
            var filePath = Path.Combine(Application.temporaryCachePath, "screenshot_" + filename + ".png");
            File.WriteAllBytes(filePath, bytes);
            callback.Invoke(filePath);

            // Clean up
            _camera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(renderTexture);
            Destroy(screenshot);

            Debug.Log("Screenshot saved to: " + filePath);            
            
            gameObject.SetActive(false);
        }
    }
}