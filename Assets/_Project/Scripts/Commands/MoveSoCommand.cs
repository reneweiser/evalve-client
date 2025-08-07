using System.Threading.Tasks;
using Evalve.Systems;
using UnityEngine;
using SceneObject = Evalve.SceneObjects.SceneObject;

namespace Evalve.Commands
{
    public class MoveSoCommand : ICommand
    {
        private readonly SceneObject _sceneObject;
        private readonly Vector3 _oldPosition;
        private readonly Vector3 _oldRotation;
        private readonly Vector3 _newPosition;
        private readonly Vector3 _newRotation;
        private readonly ObjectManager _objectManager;

        public MoveSoCommand(
            SceneObject sceneObject,
            Vector3 oldPosition,
            Vector3 oldRotation,
            Vector3 newPosition,
            Vector3 newRotation )
        {
            _sceneObject = sceneObject;
            _oldPosition = oldPosition;
            _oldRotation = oldRotation;
            _newPosition = newPosition;
            _newRotation = newRotation;
            _objectManager = Services.Get<ObjectManager>();
        }

        public async Task Execute()
        {
            _sceneObject.transform.SetPositionAndRotation(_newPosition, Quaternion.Euler(_newRotation));
            _objectManager.Refresh(_sceneObject);
            await Task.CompletedTask;
        }

        public async Task Reverse()
        {
            _sceneObject.transform.SetPositionAndRotation(_oldPosition, Quaternion.Euler(_oldRotation));
            await Task.CompletedTask;
        }
    }
}