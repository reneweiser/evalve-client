using System.Threading.Tasks;
using Evalve.Contracts;

namespace Evalve.App.Commands
{
    public class MoveObject : ICommand
    {
        private readonly IObjectManager _objectManager;

        public MoveObject(IObjectManager objectManager)
        {
            _objectManager = objectManager;
        }
        
        public Task Execute()
        {
            return Task.CompletedTask;
        }
    }
}