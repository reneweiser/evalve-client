using Evalve.Commands;
using Evalve.Panels;
using Evalve.Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using SceneObject = Evalve.SceneObjects.SceneObject;

namespace Evalve.States
{
    public class MovingObject : State
    {
        private readonly SceneCursor _cursor;
        private readonly InputAction _input;
        private readonly Info _ui;
        private readonly InputAction _cancel;
        private readonly SceneObjects.SceneObject _sceneObject;
        private string _text;
        private Vector3 _oldPosition;
        private Vector3 _oldRotation;
        private readonly ObjectManager _objectManager;

        public MovingObject(SceneObjects.SceneObject sceneObject)
        {
            _sceneObject = sceneObject;
            _input = Services.Get<InputActionAsset>()["Use"];
            _cancel = Services.Get<InputActionAsset>()["CancelUse"];
            _cursor = Services.Get<SceneCursor>();
            _ui = Services.Get<Ui>().Show<Info>();
            _text = "<color=#bada55>[LeftClick]</color> to confirm object position"
                + "\n<color=#bada55>[Esc]</color> to go back";
            _objectManager = Services.Get<ObjectManager>();
        }

        public override void Enter()
        {
            _input.canceled += ConfirmPosition;
            _cancel.started += Back;
            _objectManager.Updated += HandleObjectUpdate;
            
            _ui.SetTitle("Move object");
            _ui.SetLabel(_text);
            _sceneObject.SetIsSelected(true);
            _sceneObject.SetIsDragging(true);
            _oldPosition = _sceneObject.transform.position;
            _oldRotation = _sceneObject.transform.eulerAngles;
        }

        public override void Exit()
        {
            _input.canceled -= ConfirmPosition;
            _cancel.started -= Back;
            _objectManager.Updated -= HandleObjectUpdate;
            _sceneObject.SetIsSelected(false);
            _sceneObject.SetIsDragging(false);
        }

        public override void Update()
        {
            if (!_cursor.IsValid)
                return;
            
            if (_sceneObject == null)
                return;
            
            _sceneObject.transform.position = _cursor.Data.point;
            _ui.SetLabel(_text + "\n" + _sceneObject.transform.position);
        }

        private async void ConfirmPosition(InputAction.CallbackContext obj)
        {
            var command = new MoveSoCommand(_sceneObject,
                _oldPosition,
                _oldRotation,
                _sceneObject.transform.position,
                _sceneObject.transform.eulerAngles );
            
            await _commandBus.ExecuteCommand(command);
        }

        private void HandleObjectUpdate(SceneObject obj)
        {
            ChangeState(new ProcessingObject(new UpdateSyncSoCommand(obj), new EditingObject(obj)));
        }

        private void Back(InputAction.CallbackContext obj)
        {
            _sceneObject.transform.SetPositionAndRotation(_oldRotation, Quaternion.Euler(_oldRotation));
            ChangeState(new EditingObject(_sceneObject));
        }
    }
}