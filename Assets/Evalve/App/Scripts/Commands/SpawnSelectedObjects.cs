using System.Threading.Tasks;
using Evalve.Contracts;

namespace Evalve.App.Commands
{
    public class SpawnSelectedObjects : ICommand
    {
        private readonly ISessionManager _sessionManager;
        private readonly IObjectManager _objectManager;
        private readonly ILogger _logger;

        public SpawnSelectedObjects(ISessionManager sessionManager,
            IObjectManager objectManager,
            ILogger logger)
        {
            _sessionManager = sessionManager;
            _objectManager = objectManager;
            _logger = logger;
        }

        public Task Execute()
        {
            if (_sessionManager.SelectedObjectIds.Count == 0)
                return Task.CompletedTask;
            
            _logger.Log("Spawning selected objects", LogType.Message);
            
            foreach (var objectId in _sessionManager.SelectedObjectIds)
                _objectManager.CreateObject(_sessionManager.GetObject(objectId));
            
            return Task.CompletedTask;
        }
    }
}