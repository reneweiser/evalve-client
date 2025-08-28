using Evalve.App.Ui.Elements;

namespace Evalve.App.States.CreatingSessions
{
    public class EditingObjectView : View<EditingObjectModel>
    {
        public override void Initialize(EditingObjectModel model)
        {
            _panel.Add<Header>("Edit " + model.ObjectName);

            _panel.Add<Text>("<color=green>[Esc]</color> Cancel");

            var objectName = _panel.Add<TextInput>("Name");
            objectName.SetValue(model.ObjectName);
            objectName.InputChanged += value => Raise(new FormUpdated { FieldName = "name", Value = value });
            
            var moveObject = _panel.Add<Button>("Move Object");
            moveObject.Clicked += () => Raise(new FormUpdated { FieldName = "move_object", Value = null});
            
            var resetCamera = _panel.Add<Button>("Reset Camera");
            resetCamera.Clicked += () => Raise(new FormUpdated { FieldName = "reset_camera", Value = null});
            
            var createPose = _panel.Add<Button>("Create Pose here");
            createPose.Clicked += () => Raise(new FormUpdated { FieldName = "create_pose_here", Value = null});
            
            _panel.Add<Header>("Select Pose");
            foreach (var pose in model.ObjectPoses)
            {
                var poseButton = _panel.Add<Button>(pose.Value);
                poseButton.Clicked += () => Raise(new FormUpdated { FieldName = "select_pose", Value = pose.Key });
            }
        }

        public override void Cleanup()
        {
            _panel.Clear();
        }
    }
}