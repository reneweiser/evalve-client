using System.Collections.Generic;

namespace Evalve.App.States.CreatingSessions
{
    public class EditingPoseModel : Model
    {
        public EditingPoseModel(IObjectManager objectManager)
        {
            Roles = new Dictionary<string, string>
            {
                ["Default"] = "Default",
                ["Default-HMD"] = "Default-HMD",
                ["Moderator"] = "Moderator",
                ["Participant"] = "Participant",
            };
            
            var sObject = objectManager.GetObject(objectManager.GetSelectedObjectId());
            var selectedPose = objectManager.GetPose(objectManager.GetSelectedObjectId(), objectManager.GetSelectedPoseId());
            
            SelectedRole = selectedPose.role;
            ObjectName = sObject.name;
        }
        
        public Dictionary<string,string> Roles { get; }
        public string SelectedRole { get; }
        public string ObjectName { get; }
    }
}