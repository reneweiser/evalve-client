using System.Threading.Tasks;
using Evalve.Contracts;

namespace Evalve.App.Commands
{
    public class UpdateScreenshot : ICommand
    {
        private readonly ScreenshotCamera _screenshotCamera;
        private readonly Avatar _avatar;
        private readonly IObjectManager _objectManager;

        public UpdateScreenshot(ScreenshotCamera screenshotCamera, Avatar avatar, IObjectManager objectManager)
        {
            _screenshotCamera = screenshotCamera;
            _avatar = avatar;
            _objectManager = objectManager;
        }
        public Task Execute()
        {
            var objectId = _objectManager.GetSelectedObjectId();
            var sObject = _objectManager.GetObject(objectId);
            _screenshotCamera.transform.position = _avatar.GetCamera().transform.position;
            _screenshotCamera.transform.rotation = _avatar.GetCamera().transform.rotation;
            _screenshotCamera.CreateScreenshot(sObject.name, OnCreated);
            
            return Task.CompletedTask;
        }

        private void OnCreated(string filePath)
        {
            var objectId = _objectManager.GetSelectedObjectId();
            var sObject = _objectManager.GetObject(objectId);
            sObject.ScreenshotPath = filePath;
        }
    }
}