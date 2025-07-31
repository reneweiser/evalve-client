using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Evalve
{
    public class MoveObjectCommand : IObjectCommand
    {
        public event Action Executed;
        
        private readonly InputActionAsset _input;
        private readonly GameObject _ui;
        private readonly SceneCursor _cursor;
        private SceneObjectBehaviour _sceneObject;

        public MoveObjectCommand(InputActionAsset input, GameObject ui, SceneCursor cursor)
        {
            _input = input;
            _ui = ui;
            _cursor = cursor;

            _input["Use"].started += Prepare;
            _input["Use"].canceled += Execute;
        }

        public void Initialize()
        {
            _ui.SetActive(false);
        }

        public void Tick()
        {
            if (_sceneObject == null)
                return;
            
            _sceneObject.transform.position = _cursor.Data.point;
        }

        private void Prepare(InputAction.CallbackContext context)
        {
            var body = _cursor.Data.collider.GetComponent<BodyBehaviour>();

            if (body == null)
                return;

            _sceneObject = body.GetComponentInParent<SceneObjectBehaviour>();
        }

        private void Execute(InputAction.CallbackContext context)
        {
            _sceneObject = null;
            _ui.SetActive(true);
            _input["Use"].started -= Prepare;
            _input["Use"].canceled -= Execute;
            Executed?.Invoke();
        }
    }
}