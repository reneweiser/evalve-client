using Evalve.Contracts;
using VContainer;

namespace Evalve.App.States.CreatingSessions
{
    public class SelectingResources : IState
    {
        private readonly SelectingResourcesPresenter _presenter;
        private readonly StateMachine _stateMachine;
        private readonly IObjectResolver _container;

        public SelectingResources(
            SelectingResourcesPresenter presenter,
            StateMachine stateMachine,
            IObjectResolver container)
        {
            _presenter = presenter;
            _stateMachine = stateMachine;
            _container = container;
        }
        
        public void Enter()
        {
            _presenter.Initialize();
            _presenter.Closed += SubmitSelectedResources;
            _presenter.SetViewVisible(true);
        }

        public void Exit()
        {
            _presenter.Closed -= SubmitSelectedResources;
            _presenter.Cleanup();
            _presenter.SetViewVisible(false);
        }

        public void Update()
        {
            _presenter.Tick();
        }

        private void SubmitSelectedResources()
        {
            _stateMachine.ChangeState(_container.Resolve<CreatingScene>());
        }
    }
}