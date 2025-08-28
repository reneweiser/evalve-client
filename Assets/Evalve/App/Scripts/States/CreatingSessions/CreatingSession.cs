using Evalve.Contracts;
using VContainer;

namespace Evalve.App.States.CreatingSessions
{
    public class CreatingSession : IState
    {
        private readonly CreatingSessionPresenter _presenter;
        private readonly StateMachine _stateMachine;
        private readonly IObjectResolver _container;

        public CreatingSession(
            CreatingSessionPresenter presenter,
            StateMachine stateMachine,
            IObjectResolver container)
        {
            _presenter = presenter;
            _stateMachine = stateMachine;
            _container = container;
        }

        public void Enter()
        {
            _presenter.Closed += SubmitForm;
            _presenter.SetViewVisible(true);
            _presenter.Initialize();
        }

        public void Exit()
        {
            _presenter.Closed -= SubmitForm;
            _presenter.SetViewVisible(false);
            _presenter.Cleanup();
        }

        public void Update()
        {
            _presenter.Tick();
        }

        private void SubmitForm()
        {
            _stateMachine.ChangeState(_container.Resolve<SelectingResources>());
        }
    }
}