using Evalve.Panels;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine;
using SceneObject = Evalve.Panels.SceneObject;

namespace Evalve.States
{
    public class AddingPose : State
    {
        private readonly SceneObjects.SceneObject _sceneObject;
        private readonly AddPose _ui;
        private string _role = "Default";

        public AddingPose(StateMachine stateMachine, SceneObjects.SceneObject sceneObject) : base(stateMachine)
        {
            _sceneObject = sceneObject;
            _ui = Services.Get<SceneObject>().Show<AddPose>();
        }

        public override void Enter()
        {
            _ui.RoleSelected += SetRole;
            _ui.PoseAdded += AddPose;
            _ui.Canceled += Back;
            _sceneObject.SetIsSelected(true);
        }

        public override void Exit()
        {
            _ui.RoleSelected -= SetRole;
            _ui.PoseAdded -= AddPose;
            _ui.Canceled -= Back;
            _sceneObject.SetIsSelected(false);
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
            _stateMachine.ChangeState<EditingObject>(_sceneObject);
        }

        private void Back()
        {
            _stateMachine.ChangeState<EditingObject>(_sceneObject);
        }
    }
}