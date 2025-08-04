using Evalve.Panels;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using SceneObject = Evalve.Panels.SceneObject;

namespace Evalve.States
{
    public class EditingObject : State
    {
        private readonly SceneObjects.SceneObject _sceneObject;
        private readonly SceneObjectEdit _ui;
        private readonly InputAction _use;
        private readonly EventSystem _eventSystem;
        private bool _isMouseOverGui;

        public EditingObject(StateMachine stateMachine, SceneObjects.SceneObject sceneObject) : base(stateMachine)
        {
            _eventSystem = Services.Get<EventSystem>();
            _sceneObject = sceneObject;
            _use = Services.Get<InputActionAsset>()["Use"];
            _ui = Services.Get<SceneObject>().Show<SceneObjectEdit>();
        }

        public override void Enter()
        {
            _use.canceled += SelectObject;
            _ui.AddPoseSelected += AddPose;
            _ui.MoveSelected += Move;
            _ui.DeleteSelected += Delete;
            _ui.Canceled += Back;
            _ui.SetPosition(_sceneObject.transform.position);
            _sceneObject.SetIsSelected(true);
        }

        public override void Exit()
        {
            _use.canceled -= SelectObject;
            _ui.AddPoseSelected -= AddPose;
            _ui.MoveSelected -= Move;
            _ui.DeleteSelected -= Delete;
            _ui.Canceled -= Back;
            _sceneObject.SetIsSelected(false);
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

        private void AddPose()
        {
            _stateMachine.ChangeState<AddingPose>(_sceneObject);
        }

        private void Move()
        {
            _stateMachine.ChangeState<MovingObject>(_sceneObject);
        }

        private void Delete()
        {
            _stateMachine.ChangeState<DeletingObject>(_sceneObject);
        }

        private void Back()
        {
            _stateMachine.ChangeState<SelectingTool>();
        }
    }
}