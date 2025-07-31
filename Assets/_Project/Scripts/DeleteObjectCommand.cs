using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Evalve
{
    public class DeleteObjectCommand : IObjectCommand
    {
        public event Action Executed;
        
        private readonly InputActionAsset _inputActions;
        private readonly GameObject _ui;
        private readonly SceneCursor _cursor;

        public DeleteObjectCommand(InputActionAsset inputActions, GameObject ui, SceneCursor cursor)
        {
            _inputActions = inputActions;
            _ui = ui;
            _cursor = cursor;

            _inputActions["Use"].canceled += Execute;
        }

        public void Initialize()
        {
            _ui.SetActive(false);
        }

        public void Tick()
        {
        }

        private void Execute(InputAction.CallbackContext context)
        {
            var body = _cursor.Data.collider.GetComponent<BodyBehaviour>();

            if (body == null)
                return;

            var sceneObject = body.GetComponentInParent<SceneObjectBehaviour>();
            Object.Destroy(sceneObject.gameObject);
            _ui.SetActive(true);
            _inputActions["Use"].canceled -= Execute;
            
            Executed?.Invoke();
        }
    }
}