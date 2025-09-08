using System.Threading.Tasks;
using Evalve.Contracts;
using UnityEngine;

namespace Evalve.App.Commands
{
    public class ResetObjectCamera : ICommand
    {
        private readonly Avatar _avatar;
        private readonly IObjectManager _objectManager;

        public ResetObjectCamera(Avatar avatar, IObjectManager objectManager)
        {
            _avatar = avatar;
            _objectManager = objectManager;
        }
        
        public Task Execute()
        {
            var sObject = _objectManager.GetObject(_objectManager.GetSelectedObjectId());
            _avatar.Teleport(sObject.transform.position + new Vector3(0f, 0f, -3f), sObject.transform.eulerAngles.y);
            
            return Task.CompletedTask;
        }
    }
}