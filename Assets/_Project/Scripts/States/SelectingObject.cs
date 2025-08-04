using Evalve.Panels;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine.InputSystem;
using SceneObject = Evalve.Panels.SceneObject;

namespace Evalve.States
{
    public class SelectingObject : State
    {
        private readonly InputAction _input;
        private readonly InputAction _cancel;
        private readonly Info _ui;

        public SelectingObject(StateMachine stateMachine) : base(stateMachine)
        {
            _input = Services.Get<InputActionAsset>()["Use"];
            _cancel = Services.Get<InputActionAsset>()["CancelUse"];
            _ui = Services.Get<SceneObject>().Show<Info>();
        }

        public override void Enter()
        {
            _input.canceled += SelectObject;
            _cancel.started += Back;
            
            _ui.SetTitle("Select object");
            
            var text = "<color=#bada55>[LeftClick]</color> to select object"
                + "\n<color=#bada55>[Esc]</color> to go back";
            _ui.SetLabel(text);
        }

        public override void Exit()
        {
            _input.canceled -= SelectObject;
            _cancel.started -= Back;
        }
        public override void Update() { }

        private void SelectObject(InputAction.CallbackContext obj)
        {
            var cursor = Services.Get<SceneCursor>();
            
            if (!cursor.IsValid)
                return;

            if (!cursor.Data.collider.TryGetComponent(typeof(Handle), out var body))
                return;

            var sceneObject = body.GetComponentInParent<SceneObjects.SceneObject>();
            
            _stateMachine.ChangeState<EditingObject>(sceneObject);
        }

        private void Back(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState<SelectingTool>();
        }
    }
}