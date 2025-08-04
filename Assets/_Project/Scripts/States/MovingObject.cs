using Evalve.Panels;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using SceneObject = Evalve.Panels.SceneObject;

namespace Evalve.States
{
    public class MovingObject : State
    {
        private readonly SceneCursor _cursor;
        private readonly InputAction _input;
        private readonly Info _ui;
        private readonly InputAction _cancel;
        private readonly SceneObjects.SceneObject _sceneObject;
        private readonly Vector3 _oldPosition;
        private readonly Quaternion _oldRotation;
        private string _text;

        public MovingObject(StateMachine stateMachine, SceneObjects.SceneObject sceneObject) : base(stateMachine)
        {
            _sceneObject = sceneObject;
            _input = Services.Get<InputActionAsset>()["Use"];
            _cancel = Services.Get<InputActionAsset>()["CancelUse"];
            _cursor = Services.Get<SceneCursor>();
            _ui = Services.Get<SceneObject>().Show<Info>();
            _oldPosition = _sceneObject.transform.position;
            _oldRotation = _sceneObject.transform.rotation;
            _text = "<color=#bada55>[LeftClick]</color> to confirm object position"
                + "\n<color=#bada55>[Esc]</color> to go back";
            
        }

        public override void Enter()
        {
            _input.canceled += ConfirmPosition;
            _cancel.started += Back;
            
            _ui.SetTitle("Move object");
            _ui.SetLabel(_text);
            _sceneObject.SetIsSelected(true);
            _sceneObject.SetIsDragging(true);
        }

        public override void Exit()
        {
            _input.canceled -= ConfirmPosition;
            _cancel.started -= Back;
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

        private void ConfirmPosition(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState<EditingObject>(_sceneObject);
        }

        private void Back(InputAction.CallbackContext obj)
        {
            _sceneObject.transform.position = _oldPosition;
            _sceneObject.transform.rotation = _oldRotation;
            _stateMachine.ChangeState<EditingObject>(_sceneObject);
        }
    }
}