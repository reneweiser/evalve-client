using System.Threading.Tasks;
using Evalve.Contracts;

namespace Evalve.App.Commands
{
    public class DeleteSelectedPose : ICommand
    {
        private readonly IObjectManager _objectManager;

        public DeleteSelectedPose(IObjectManager objectManager)
        {
            _objectManager = objectManager;
        }
        
        public Task Execute()
        {
            var sObjectId = _objectManager.GetSelectedObjectId();
            var poseId = _objectManager.GetSelectedPoseId();
            
            _objectManager.DeletePose(sObjectId, poseId);
            
            return Task.CompletedTask;
        }
    }
}