using Evalve.App.Commands;
using Evalve.Contracts;
using VContainer;

namespace Evalve.App.States.CreatingSessions
{
    public class EditingPose : IState
    {
        private readonly EditingPosePresenter _presenter;
        private readonly StateMachine _stateMachine;
        private readonly IObjectResolver _container;

        public EditingPose(EditingPosePresenter presenter,
            StateMachine stateMachine,
            IObjectResolver container)
        {
            _presenter = presenter;
            _stateMachine = stateMachine;
            _container = container;
        }
        
        public void Enter()
        {
            _presenter.Closed += Back;
            _presenter.Initialize();
            _presenter.SetViewVisible(true);
        }

        public void Exit()
        {
            _presenter.Closed -= Back;
            _presenter.SetViewVisible(false);
            _presenter.Cleanup();
        }

        public void Update()
        {
            _presenter.Tick();
        }

        private void Back()
        {
            _container.Resolve<UpdateSelectedSceneObject>().Execute();
            _stateMachine.ChangeState(_container.Resolve<EditingObject>());
        }
    }
}