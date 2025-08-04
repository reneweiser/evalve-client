using Evalve.Panels;
using Evalve.Systems;
using UnityEngine.InputSystem;

namespace Evalve.States
{
    public class IdleUi : State
    {
        private readonly InputAction _input;
        private bool _isVisible;

        public IdleUi(StateMachine stateMachine) : base(stateMachine)
        {
            Services.Get<SceneObject>().Show<Idle>();
            _input = Services.Get<InputActionAsset>()["Menu"];
        }

        public override void Enter()
        {
            _input.started += OpenMenu;
        }

        public override void Exit()
        {
            _input.started -= OpenMenu;
        }

        public override void Update() { }

        private void OpenMenu(InputAction.CallbackContext context)
        {
            _stateMachine.ChangeState<SelectingTool>();
        }
    }
}