using System.Threading.Tasks;
using Evalve.Contracts;

namespace Evalve.App.Commands
{
    public class MoveAvatarToPose : ICommand
    {
        private readonly Avatar _avatar;
        private readonly IObjectManager _objectManager;

        public MoveAvatarToPose(Avatar avatar, IObjectManager objectManager)
        {
            _avatar = avatar;
            _objectManager = objectManager;
        }
        
        public Task Execute()
        {
            var sObjectId = _objectManager.GetSelectedObjectId();
            var poseId = _objectManager.GetSelectedPoseId();

            var pose = _objectManager.GetPose(sObjectId, poseId);
            
            _avatar.Teleport(pose.transform.position, pose.transform.eulerAngles.y);
            
            return Task.CompletedTask;
        }
    }
}