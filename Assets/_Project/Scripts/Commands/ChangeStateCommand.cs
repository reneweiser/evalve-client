using System.Threading.Tasks;
using Evalve.Systems;

namespace Evalve.Commands
{
    public class ChangeStateCommand : ICommand
    {
        private readonly UiStateMachine _stateMachine;
        private readonly State _from;
        private readonly State _to;

        public ChangeStateCommand(UiStateMachine stateMachine, State from, State to)
        {
            _stateMachine = stateMachine;
            _from = from;
            _to = to;
        }
        
        public Task Execute()
        {
            _stateMachine.ChangeState(_to);
            return Task.CompletedTask;
        }

        public Task Reverse()
        {
            _stateMachine.ChangeState(_from);
            return Task.CompletedTask;
        }
    }
}