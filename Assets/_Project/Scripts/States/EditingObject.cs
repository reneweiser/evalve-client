using Evalve.Panels;
using Evalve.Panels.Elements;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine.InputSystem;
using SceneObject = Evalve.Panels.SceneObject;

namespace Evalve.States
{
    public class EditingObject : State
    {
        private readonly SceneObjects.SceneObject _sceneObject;
        private readonly Info _ui;
        private readonly InputAction _use;
        private bool _isMouseOverGui;
        private readonly Actions _actions;

        public EditingObject(StateMachine stateMachine, SceneObjects.SceneObject sceneObject) : base(stateMachine)
        {
            _sceneObject = sceneObject;
            _use = Services.Get<InputActionAsset>()["Use"];
            _ui = Services.Get<SceneObject>().Show<Info>();
            _actions = _ui.GetComponent<Actions>();
        }

        public override void Enter()
        {
            _use.canceled += SelectObject;
            
            _actions.AddAction("Poses", () => _stateMachine.ChangeState<AddingPose>(_sceneObject));
            _actions.AddAction("Move", () => _stateMachine.ChangeState<MovingObject>(_sceneObject));
            _actions.AddAction("Delete", () => _stateMachine.ChangeState<DeletingObject>(_sceneObject));

            _ui.SetTitle("Editing " + _sceneObject.name);
            
            _sceneObject.SetIsSelected(true);
        }

        public override void Exit()
        {
            _use.canceled -= SelectObject;
            _sceneObject.SetIsSelected(false);
            
            _actions.RemoveAction("Poses");
            _actions.RemoveAction("Move");
            _actions.RemoveAction("Delete");
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
    }
}