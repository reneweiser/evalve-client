using Evalve.App.States.CreatingSessions;
using VContainer;
using VContainer.Unity;

namespace Evalve.App
{
    public class Bootstrap : IStartable, IFixedTickable
    {
        private readonly IObjectResolver _container;
        private readonly StateMachine _stateMachine;

        public Bootstrap(IObjectResolver container, StateMachine stateMachine)
        {
            _container = container;
            _stateMachine = stateMachine;
        }
        
        public void Start()
        {
            _stateMachine.ChangeState(_container.Resolve<CreatingSession>());
        }

        public void FixedTick()
        {
            _stateMachine.Tick();
        }
    }
}
