using System.Collections.Generic;
using System.Linq;
using Evalve.App.Ui.Elements;

namespace Evalve.App.States.CreatingSessions
{
    public class EditingPoseView : View<EditingPoseModel>
    {
        private Select _role;

        private Dictionary<string, List<Element>> _tabs;

        public override void Initialize(EditingPoseModel model)
        {
            _panel.Add<Header>("Editing Pose");

            var description = _panel.Add<Text>($"[{model.ObjectName}] > [{model.SelectedRole}]");
            
            _role = _panel.Add<Select>("Role");
            _role.SetOptions(model.Roles);
            _role.SetValue(model.SelectedRole);
            
            _role.InputChanged += role => Raise(new ViewEvent { Key = "role_changed", Value = role });
            
            var move = _panel.Add<Button>("Move pose here");
            move.Clicked += () => Raise(new ViewEvent { Key = "move_pose_here", Value = null });
            
            var toPose = _panel.Add<Button>("Move to pose");
            toPose.Clicked += () => Raise(new ViewEvent { Key = "move_to_pose", Value = null });
            
            var delete = _panel.Add<Button>("Delete");
            delete.Clicked += () => EnableTab("delete");
            
            var save = _panel.Add<Button>("Confirm");
            save.Clicked += () => Raise(new FormConfirmed());
            
            var deleteText = _panel.Add<Text>("Are you sure you want to delete this pose?");
            
            var deleteCancel = _panel.Add<Button>("No");
            deleteCancel.Clicked += () => EnableTab("default");
            
            var deleteConfirm = _panel.Add<Button>("Yes");
            deleteConfirm.Clicked += () => Raise(new ViewEvent { Key = "deleted", Value = null });

            _tabs = new Dictionary<string, List<Element>>()
            {
                ["default"] = new() { description, _role, move, toPose, delete, save },
                ["delete"] = new() { deleteText, deleteCancel, deleteConfirm },
            };
            
            EnableTab("default");
        }

        private void EnableTab(string tabName)
        {
            foreach (var element in _tabs.SelectMany(tab => tab.Value))
            {
                element.gameObject.SetActive(false);
            }

            foreach (var element in _tabs[tabName])
            {
                element.gameObject.SetActive(true);
            }
        }

        public override void Cleanup()
        {
            _panel.Clear();
        }
    }
}