using System.Threading.Tasks;
using Evalve.Contracts;

namespace Evalve.App.Commands
{
    public class MovePoseToAvatar : ICommand
    {
        private readonly Avatar _avatar;
        private readonly IObjectManager _objectManager;

        public MovePoseToAvatar(Avatar avatar, IObjectManager objectManager)
        {
            _avatar = avatar;
            _objectManager = objectManager;
        }
        
        public Task Execute()
        {
            var sObjectId = _objectManager.GetSelectedObjectId();
            var poseId = _objectManager.GetSelectedPoseId();
            _objectManager.MovePoseAt(sObjectId, poseId, _avatar.transform.position, _avatar.transform.eulerAngles);
            
            return Task.CompletedTask;
        }
    }
}