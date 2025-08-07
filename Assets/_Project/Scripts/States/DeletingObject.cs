using Evalve.Commands;
using Evalve.Panels;
using Evalve.Panels.Elements;
using Evalve.SceneObjects;
using Evalve.Systems;

namespace Evalve.States
{
    public class DeletingObject : State
    {
        private readonly SceneObject _sceneObject;
        private readonly Info _ui;
        private readonly Actions _actions;

        public DeletingObject(SceneObject sceneObject)
        {
            _sceneObject = sceneObject;
            _ui = Services.Get<Ui>().Show<Info>();
            _actions = _ui.GetComponent<Actions>();
        }

        public override void Enter()
        {
            _ui.SetTitle("Deleting object");
            _ui.SetLabel("<color=red>This action will be permanent.</color>");
            _sceneObject.SetIsSelected(true);
            
            _actions.AddAction("Confirm", DeleteObject);
            _actions.AddAction("Back", () => ChangeState(new EditingObject(_sceneObject)));
        }

        public override void Exit()
        {
            _sceneObject.SetIsSelected(false);
            _actions.RemoveAction("Confirm");
            _actions.RemoveAction("Back");
        }

        public override void Update() { }

        private void DeleteObject()
        {
            var data = _sceneObject.Data;
            Services.Get<ObjectManager>().Delete(_sceneObject);
            ChangeState(new ProcessingObject(new DeleteSyncSoCommand(data), new Idle()));
        }
    }
}