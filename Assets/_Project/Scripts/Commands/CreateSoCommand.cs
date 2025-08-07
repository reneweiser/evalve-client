using System.Threading.Tasks;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine;

namespace Evalve.Commands
{
    public class CreateSoCommand : ICommand
    {
        private readonly Vector3 _position;
        private readonly Factory _factory;
        private readonly ObjectManager _objectManager;
        
        private SceneObject _sceneObject;

        public CreateSoCommand(Vector3 position)
        {
            _position = position;
            _factory = Services.Get<Factory>();
            _objectManager = Services.Get<ObjectManager>();
        }
        
        public Task Execute()
        {
            _sceneObject = _factory.CreateNewAt(_position);
            _sceneObject.SetIsActive(true);
            _objectManager.Create(_sceneObject);
            
            return Task.CompletedTask;
        }

        public Task Reverse()
        {
            return Task.CompletedTask;
        }
    }
}