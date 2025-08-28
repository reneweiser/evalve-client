using System;
using Evalve.Contracts;

namespace Evalve.App
{
    public class StateMachine
    {
        private readonly ILogger _logger;
        private IState _currentState;

        public StateMachine(ILogger logger)
        {
            _logger = logger;
        }

        public void ChangeState(IState newState)
        {
            try
            {
                _currentState?.Exit();
                _currentState = newState;
                _currentState?.Enter();
            }
            catch (Exception e)
            {
                _logger.Log(e.Message, LogType.Error);
                throw;
            }
        }

        public void Tick()
        {
            _currentState?.Update();
        }
    }
}