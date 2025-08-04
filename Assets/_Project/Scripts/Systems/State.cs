namespace Evalve.Systems
{
    public abstract class State
    {
        protected StateMachine _stateMachine;

        protected State(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}