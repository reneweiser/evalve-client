using System.Threading.Tasks;
using Evalve.Client;
using Evalve.SceneObjects;
using Evalve.Systems;
using SceneObject = Evalve.Client.SceneObject;

namespace Evalve.Commands
{
    public class CreateSyncSoCommand : ICommand
    {
        private readonly SceneObject _sceneObjectData;
        private readonly SceneObjectSerializer _serializer;
        private readonly Connection _connection;
        private readonly Factory _factory;
        private readonly ObjectManager _objectManager;

        public CreateSyncSoCommand(SceneObject sceneObjectData)
        {
            _sceneObjectData = sceneObjectData;
            _serializer = Services.Get<SceneObjectSerializer>();
            _connection = Services.Get<Connection>();
            _factory = Services.Get<Factory>();
            _objectManager = Services.Get<ObjectManager>();
        }
        public async Task Execute()
        {
            var sceneObjectData = await _connection.CreateSceneObjectAsync(_sceneObjectData);
            
            var sceneObject = _factory.CreateFromData(sceneObjectData);
            sceneObject.SetIsActive(true);
            _objectManager.Create(sceneObject);
        }

        public Task Reverse()
        {
            return Task.CompletedTask;
        }
    }
}