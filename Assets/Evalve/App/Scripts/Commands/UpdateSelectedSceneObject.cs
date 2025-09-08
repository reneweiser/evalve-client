using System.Threading.Tasks;
using Evalve.Contracts;
using UnityEngine;

namespace Evalve.App.Commands
{
    public class UpdateSelectedSceneObject : ICommand
    {
        private readonly ISessionManager _sessionManager;
        private readonly IObjectManager _objectManager;

        public UpdateSelectedSceneObject(ISessionManager sessionManager, IObjectManager objectManager)
        {
            _sessionManager = sessionManager;
            _objectManager = objectManager;
        }
        
        public Task Execute()
        {
            var sObject = _objectManager.GetObject(_objectManager.GetSelectedObjectId());
            _sessionManager.UpdateObject(sObject.GetId(), sObject);
            
            Debug.Log("Objected updated");
            return Task.CompletedTask;
        }
    }
}