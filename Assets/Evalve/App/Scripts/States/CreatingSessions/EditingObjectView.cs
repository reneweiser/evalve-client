using Evalve.App.Ui.Elements;

namespace Evalve.App.States.CreatingSessions
{
    public class EditingObjectView : View<EditingObjectModel>
    {
        public override void Initialize(EditingObjectModel model)
        {
            _panel.Add<Header>("Edit " + model.ObjectName);

            var objectName = _panel.Add<TextInput>("Name");
            objectName.SetValue(model.ObjectName);
            objectName.InputChanged += value => Raise(new ViewEvent { Key = "name", Value = value });
            
            var updateScreenshot = _panel.Add<Button>("Update Screenshot");
            updateScreenshot.Clicked += () => Raise(ViewEvent.Create("update_screenshot", "update_screenshot"));
            
            var moveObject = _panel.Add<Button>("Move Object");
            moveObject.Clicked += () => Raise(new ViewEvent { Key = "move_object", Value = null});
            
            var resetCamera = _panel.Add<Button>("Reset Camera");
            resetCamera.Clicked += () => Raise(new ViewEvent { Key = "reset_camera", Value = null});
            
            var createPose = _panel.Add<Button>("Create Pose here");
            createPose.Clicked += () => Raise(new ViewEvent { Key = "create_pose_here", Value = null});
            
            _panel.Add<Header>("Select Pose");
            foreach (var pose in model.ObjectPoses)
            {
                var poseButton = _panel.Add<Button>(pose.Value);
                poseButton.Clicked += () => Raise(new ViewEvent { Key = "select_pose", Value = pose.Key });
            }

            _panel.Add<Header>("Help");
            _panel.Add<Text>("<color=green>[Esc]</color> Cancel");
        }

        public override void Cleanup()
        {
            _panel.Clear();
        }
    }
}