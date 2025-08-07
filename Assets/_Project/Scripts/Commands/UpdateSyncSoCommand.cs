using System.Threading.Tasks;
using Evalve.Client;
using Evalve.Systems;
using SceneObject = Evalve.SceneObjects.SceneObject;

namespace Evalve.Commands
{
    public class UpdateSyncSoCommand : ICommand
    {
        private readonly SceneObject _sceneObject;
        private readonly Connection _connection;
        private readonly SceneObjectSerializer _serializer;

        public UpdateSyncSoCommand(SceneObject sceneObject)
        {
            _sceneObject = sceneObject;
            _serializer = Services.Get<SceneObjectSerializer>();
            _connection = Services.Get<Connection>();
        }
        
        public async Task Execute()
        {
            var so = _serializer.Serialize(_sceneObject);
            await _connection.UpdateSceneObjectAsync(so.Id, so);
        }

        public Task Reverse()
        {
            return Task.CompletedTask;
        }
    }
}