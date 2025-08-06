using Evalve.Panels;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine;

namespace Evalve.States
{
    public class DeletingObject : State
    {
        private readonly SceneObjects.SceneObject _sceneObject;
        private readonly SceneObjectDelete _ui;

        public DeletingObject(StateMachine stateMachine, SceneObjects.SceneObject sceneObject) : base(stateMachine)
        {
            _sceneObject = sceneObject;
            _ui = Services.Get<Ui>().Show<SceneObjectDelete>();
        }

        public override void Enter()
        {
            _ui.ObjectDeleted += DeleteObject;
            _ui.Canceled += Back;
            _sceneObject.SetIsSelected(true);
        }

        public override void Exit()
        {
            _ui.ObjectDeleted -= DeleteObject;
            _ui.Canceled -= Back;
            _sceneObject.SetIsSelected(false);
        }

        public override void Update() { }

        private void DeleteObject()
        {
            Object.Destroy(_sceneObject.gameObject);
            _stateMachine.ChangeState<SelectingObject>();
        }

        private void Back()
        {
            _stateMachine.ChangeState<EditingObject>(_sceneObject);
        }
    }
}