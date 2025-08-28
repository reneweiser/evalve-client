using System.Threading.Tasks;
using Evalve.Contracts;

namespace Evalve.App.Commands
{
    public class CreatePose : ICommand
    {
        private readonly Avatar _avatar;
        private readonly IObjectManager _objectManager;

        public CreatePose(Avatar avatar, IObjectManager objectManager)
        {
            _avatar = avatar;
            _objectManager = objectManager;
        }
        
        public Task Execute()
        {
            var objectId = _objectManager.GetSelectedObjectId();
            _objectManager.CreatePoseAt(objectId, _avatar.transform.position, _avatar.transform.eulerAngles);
            
            return Task.CompletedTask;
        }
    }
}