using System.Threading.Tasks;
using Evalve.App.Ui.Elements;
using Evalve.Contracts;
using UnityEngine;

namespace Evalve.App.Commands
{
    public class UpdateSelectedSceneObject : ICommand
    {
        private readonly ISessionManager _sessionManager;
        private readonly IObjectManager _objectManager;
        private readonly Notifications _notifications;

        public UpdateSelectedSceneObject(ISessionManager sessionManager, IObjectManager objectManager, Notifications notifications)
        {
            _sessionManager = sessionManager;
            _objectManager = objectManager;
            _notifications = notifications;
        }
        
        public Task Execute()
        {
            var sObject = _objectManager.GetObject(_objectManager.GetSelectedObjectId());
            _sessionManager.UpdateObject(sObject.GetId(), sObject);
            
            _notifications.AddNotification("Object updated");
            return Task.CompletedTask;
        }
    }
}