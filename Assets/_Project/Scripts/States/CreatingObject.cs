using System;
using Evalve.Client;
using Evalve.Commands;
using Evalve.Panels;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;
using SceneObject = Evalve.SceneObjects.SceneObject;

namespace Evalve.States
{
    public class CreatingObject : State
    {
        private readonly SceneObject _preview;
        private readonly SceneCursor _cursor;
        private readonly InputAction _input;
        private readonly InputAction _cancel;
        private readonly Info _ui;

        public CreatingObject()
        {
            _cursor = Services.Get<SceneCursor>();
            _preview = Services.Get<Factory>().CreateTemporary(_cursor.Data.point);
            _input = Services.Get<InputActionAsset>()["Use"];
            _cancel = Services.Get<InputActionAsset>()["CancelUse"];
            _ui = Services.Get<Ui>().Show<Info>();
        }

        public override void Enter()
        {
            _ui.SetTitle("Placing object");
            _ui.SetLabel("<color=#bada55>[LeftClick]</color> to place object");
            _preview.SetIsActive(true);
            _preview.SetIsDragging(true);
            
            _input.canceled += PlaceObject;
            _cancel.started += Back;
        }

        public override void Exit()
        {
            Object.Destroy(_preview.gameObject);
            _input.canceled -= PlaceObject;
            _cancel.started -= Back;
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
            var data = new Client.SceneObject()
            {
                TeamId = "01k1atqfmqzms79n6erv1k4dq2",
                Name = "New Scene Object",
                Transform = new Client.Transform
                {
                    Position = _preview.transform.position.ToVector(),
                    Rotation = Vector3.zero.ToVector(),
                },
                Properties = new Client.Property[] { }
            };
            ChangeState(new ProcessingObject(new CreateSyncSoCommand(data), new Idle()));
        }

        private void Back(InputAction.CallbackContext obj)
        {
            ChangeState(new Idle());
        }
    }
}