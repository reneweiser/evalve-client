using Evalve.Panels;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine.InputSystem;
using SceneObject = Evalve.Panels.SceneObject;

namespace Evalve.States
{
    public class PlacingObject : State
    {
        private SceneObjects.SceneObject _preview;
        private readonly SceneCursor _cursor;
        private readonly InputAction _input;
        private readonly Info _ui;

        public PlacingObject(StateMachine stateMachine) : base(stateMachine)
        {
            _cursor = Services.Get<SceneCursor>();
            _preview = Services.Get<Spawner>().SpawnAt(_cursor.Data.point);
            _input = Services.Get<InputActionAsset>()["Use"];
            _ui = Services.Get<SceneObject>().Show<Info>();
        }

        public override void Enter()
        {
            _ui.SetTitle("Placing object");
            _ui.SetLabel("<color=#bada55>[LeftClick]</color> to place object");
            _preview.SetIsActive(true);
            _preview.SetIsDragging(true);
            
            _input.canceled += PlaceObject;
        }

        public override void Exit()
        {
            _preview.SetIsDragging(false);
            _preview = null;
            _input.canceled -= PlaceObject;
        }

        public override void Update()
        {
            if (!_cursor.IsValid)
                return;

            if (_preview == null)
                return;
            
            _preview.transform.position = _cursor.Data.point;
        }

        private void PlaceObject(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState<EditingObject>(_preview);
        }
    }
}