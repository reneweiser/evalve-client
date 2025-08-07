using Evalve.Panels;
using Evalve.Systems;

namespace Evalve.States
{
    public class ProcessingObject : State
    {
        private readonly ICommand _processCommand;
        private readonly State _next;
        private readonly Info _info;

        public ProcessingObject(ICommand processCommand, State next)
        {
            _processCommand = processCommand;
            _next = next;
            _info = Services.Get<Ui>().Get<Info>();
        }

        public override async void Enter()
        {
            _info.SetTitle("Processing object");
            _info.SetLabel("Synchronizing object with database.");
            
            await _commandBus.ExecuteCommand(_processCommand);
            
            ChangeState(_next);
        }

        public override void Exit() { }

        public override void Update() { }
    }
}