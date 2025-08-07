using Evalve.Panels;
using Evalve.Panels.Elements;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine.InputSystem;

namespace Evalve.States
{
    public class EditingObject : State
    {
        private readonly SceneObjects.SceneObject _sceneObject;
        private readonly Info _ui;
        private bool _isMouseOverGui;
        private readonly Actions _actions;

        public EditingObject(SceneObjects.SceneObject sceneObject)
        {
            _sceneObject = sceneObject;
            _ui = Services.Get<Ui>().Show<Info>();
            _actions = _ui.GetComponent<Actions>();
        }

        public override void Enter()
        {
            _inputUse.canceled += SelectObject;
            _inputMenu.started += OpenMenu;
            
            _ui.SetTitle("Editing " + _sceneObject.name);
            var description = "Name: " + _sceneObject.name
                + "\nPosition: " + _sceneObject.transform.position
                + "\nRotation: " + _sceneObject.transform.rotation;
            _ui.SetLabel(description);
            _actions.AddAction("Poses", () => ChangeState(new ManagingPoses(_sceneObject)));
            _actions.AddAction("Move", () => ChangeState(new MovingObject(_sceneObject)));
            _actions.AddAction("Delete", () => ChangeState(new DeletingObject(_sceneObject)));

            
            _sceneObject.SetIsSelected(true);
        }

        public override void Exit()
        {
            _inputUse.canceled -= SelectObject;
            _inputMenu.started -= OpenMenu;
            _sceneObject.SetIsSelected(false);
            
            _actions.RemoveAction("Poses");
            _actions.RemoveAction("Move");
            _actions.RemoveAction("Delete");
        }

        public override void Update() { }
    }
}