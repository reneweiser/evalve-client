using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Evalve
{
    public class SpawnObjectCommand : IObjectCommand
    {
        public event Action Executed;
        
        private readonly InputActionAsset _input;
        private readonly SceneObjectSpawner _spawner;
        private readonly GameObject _ui;
        private readonly SceneCursor _cursor;
        private SceneObjectBehaviour _preview;

        public SpawnObjectCommand(InputActionAsset input, SceneObjectSpawner spawner, GameObject ui, SceneCursor cursor)
        {
            _input = input;
            _spawner = spawner;
            _ui = ui;
            _cursor = cursor;

            _input["Use"].canceled += Execute;
        }

        public void Initialize()
        {
            _preview = _spawner.SpawnAt(_cursor.Data.point);
            _ui.SetActive(false);
        }

        public void Tick()
        {
            _preview.transform.position = _cursor.Data.point;
        }

        private void Execute(InputAction.CallbackContext context)
        {
            _spawner.SpawnAt(_cursor.Data.point);
            Object.Destroy(_preview.gameObject);
            _ui.SetActive(true);
            _input["Use"].canceled -= Execute;
            Executed?.Invoke();
        }
    }
}