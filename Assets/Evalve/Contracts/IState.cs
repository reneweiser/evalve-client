namespace Evalve.Contracts
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
    }
}