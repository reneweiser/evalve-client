using Evalve.Panels;
using Evalve.Systems;

namespace Evalve.States
{
    public class SplashScreen : State
    {
        private readonly Panels.SplashScreen _ui;

        public SplashScreen(StateMachine stateMachine) : base(stateMachine)
        {
            _ui = Services.Get<SceneObject>().Show<Panels.SplashScreen>();
        }

        public override void Enter()
        {
            _ui.Confirmed += OnConfirmed;
        }

        public override void Exit()
        {
            _ui.Confirmed -= OnConfirmed;
        }

        public override void Update() { }

        private void OnConfirmed()
        {
            _stateMachine.ChangeState<IdleUi>();
        }
    }
}