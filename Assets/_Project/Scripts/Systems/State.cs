using Evalve.SceneObjects;
using Evalve.States;
using UnityEngine.InputSystem;
using SceneObject = Evalve.SceneObjects.SceneObject;

namespace Evalve.Systems
{
    public abstract class State
    {
        protected readonly StateMachine _stateMachine;
        protected readonly InputAction _inputUse;
        protected readonly InputAction _inputMenu;

        protected State(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            var inputs = Services.Get<InputActionAsset>();
            _inputUse = inputs["Use"];
            _inputMenu = inputs["Menu"];
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();

        protected void OpenMenu(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState<UsingElementsMenu>();
        }

        protected void SelectObject(InputAction.CallbackContext obj)
        {
            var cursor = Services.Get<SceneCursor>();
            
            if (!cursor.IsValid)
                return;

            if (!cursor.Data.collider.TryGetComponent(typeof(Handle), out var body))
            {
                _stateMachine.ChangeState<Idle>();
                return;
            }

            var sceneObject = body.GetComponentInParent<SceneObject>();
            
            _stateMachine.ChangeState<EditingObject>(sceneObject);
        }
    }
}