using Evalve.Panels;
using Evalve.Systems;

namespace Evalve.States
{
    public class UsingElementsMenu : State
    {
        private readonly ElementsMenu _menu;

        public UsingElementsMenu(StateMachine stateMachine) : base(stateMachine)
        {
            _menu = Services.Get<SceneObject>().Show<ElementsMenu>();
        }

        public override void Enter()
        {
            _menu.Hide += HideMenu;
        }

        public override void Exit()
        {
            _menu.Hide -= HideMenu;
        }

        public override void Update() { }

        private void HideMenu()
        {
            _stateMachine.ChangeState<IdleUi>();
        }
    }
}