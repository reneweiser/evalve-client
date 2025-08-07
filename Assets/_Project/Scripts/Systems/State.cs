using Evalve.Commands;
using Evalve.SceneObjects;
using Evalve.States;
using UnityEngine.InputSystem;
using SceneObject = Evalve.SceneObjects.SceneObject;

namespace Evalve.Systems
{
    public abstract class State
    {
        protected readonly UiStateMachine _stateMachine;
        protected readonly InputAction _inputUse;
        protected readonly InputAction _inputMenu;
        protected readonly CommandBus _commandBus;

        protected State()
        {
            _stateMachine = Services.Get<UiStateMachine>();
            _commandBus = Services.Get<CommandBus>();
            var inputs = Services.Get<InputActionAsset>();
            _inputUse = inputs["Use"];
            _inputMenu = inputs["Menu"];
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();

        protected void OpenMenu(InputAction.CallbackContext obj)
        {
            ChangeState(new UsingElementsMenu());
        }

        protected async void ChangeState(State newState)
        {
            await _commandBus.ExecuteCommand(new ChangeStateCommand(_stateMachine, this, newState));
        }

        protected void SelectObject(InputAction.CallbackContext obj)
        {
            var cursor = Services.Get<SceneCursor>();
            
            if (!cursor.IsValid)
                return;

            if (!cursor.Data.collider.TryGetComponent(typeof(Handle), out var body))
            {
                ChangeState(new Idle());
                return;
            }

            var sceneObject = body.GetComponentInParent<SceneObject>();
            
            ChangeState(new EditingObject(sceneObject));
        }
    }
}