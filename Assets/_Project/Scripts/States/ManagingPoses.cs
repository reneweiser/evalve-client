using Evalve.Panels;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine;

namespace Evalve.States
{
    public class ManagingPoses : State
    {
        private readonly SceneObjects.SceneObject _sceneObject;
        private readonly AddPose _ui;
        private readonly ListPoses _listPoses;
        private string _role = "Default";

        public ManagingPoses(SceneObjects.SceneObject sceneObject)
        {
            _sceneObject = sceneObject;
            _ui = Services.Get<Ui>().Show<AddPose>();
            _listPoses = Services.Get<Ui>().Show<ListPoses>();
        }

        public override void Enter()
        {
            _ui.RoleSelected += SetRole;
            _ui.PoseAdded += AddPose;
            _ui.Canceled += Back;
            _sceneObject.SetIsSelected(true);
            
            foreach (var pose in _sceneObject.GetPoses())
            {
                _listPoses.AddPose(pose);
            }
        }

        public override void Exit()
        {
            _ui.RoleSelected -= SetRole;
            _ui.PoseAdded -= AddPose;
            _ui.Canceled -= Back;
            _sceneObject.SetIsSelected(false);
            _listPoses.Clear();
        }

        public override void Update() { }

        private void SetRole(string role)
        {
            _role = role;
        }

        private void AddPose()
        {
            var camera = Services.Get<Camera>();
            _sceneObject.AddPose(_role, camera.transform.position, camera.transform.rotation);
            ChangeState(new ManagingPoses(_sceneObject));
        }

        private void Back()
        {
            ChangeState(new EditingObject(_sceneObject));
        }
    }
}