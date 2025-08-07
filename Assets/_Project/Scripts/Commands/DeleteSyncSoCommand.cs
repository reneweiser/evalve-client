using System.Threading.Tasks;
using Evalve.Client;
using Evalve.Systems;

namespace Evalve.Commands
{
    public class DeleteSyncSoCommand : ICommand
    {
        private readonly Connection _connection;
        private readonly SceneObject _sceneObject;

        public DeleteSyncSoCommand(SceneObject sceneObject)
        {
            _sceneObject = sceneObject;
            _connection = Services.Get<Connection>();
        }

        public async Task Execute()
        {
            await _connection.DeleteSceneObjectAsync(_sceneObject.Id);
        }

        public Task Reverse()
        {
            return Task.CompletedTask;
        }
    }
}