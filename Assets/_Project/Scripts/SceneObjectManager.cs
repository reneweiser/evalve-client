using UnityEngine;
using UnityEngine.InputSystem;

namespace Evalve
{
    public class SceneObjectManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;
        [SerializeField] private SceneCursor _sceneCursor;
        [SerializeField] private SceneObjectSpawner _sceneObjectSpawner;
        [SerializeField] private GameObject _ui;
        private IObjectCommand _pendingCommand;

        public void PlaceSceneObject()
        {
            _pendingCommand = new SpawnObjectCommand(_inputActions, _sceneObjectSpawner, _ui, _sceneCursor);
            _pendingCommand.Executed += () => _pendingCommand = null;
            _pendingCommand.Initialize();
        }

        public void MoveSceneObject()
        {
            _pendingCommand = new MoveObjectCommand(_inputActions, _ui, _sceneCursor);
            _pendingCommand.Executed += () => _pendingCommand = null;
            _pendingCommand.Initialize();
        }

        public void DeleteSceneObject()
        {
            _pendingCommand = new DeleteObjectCommand(_inputActions, _ui, _sceneCursor);
            _pendingCommand.Executed += () => _pendingCommand = null;
            _pendingCommand.Initialize();
        }

        private void FixedUpdate()
        {
            _pendingCommand?.Tick();
        }
    }
}