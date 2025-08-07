using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evalve.Systems
{
    public class CommandBus
    {
        private readonly Stack<ICommand> _commands;

        public CommandBus()
        {
            _commands = new Stack<ICommand>();
        }
        
        public async Task ExecuteCommand(ICommand command)
        {
            try
            {
                await command.Execute();
                _commands.Push(command);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                await command.Reverse();
                throw;
            }
        }

        public void ReverseCommand()
        {
            _commands.Pop().Reverse();
        }
    }
}