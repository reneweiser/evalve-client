using Evalve.App.Commands;
using Evalve.Contracts;
using UnityEngine.InputSystem;
using VContainer;

namespace Evalve.App.States
{
    public class Idle : IState
    {
        private readonly IObjectResolver _container;
        private readonly InputActionAsset _input;

        public Idle(IObjectResolver container, InputActionAsset input) {
            _container = container;
            _input = input;
        }
        
        public void Enter()
        {
            _input["Use"].canceled += SelectObject;
        }

        public void Exit()
        {
            _input["Use"].canceled -= SelectObject;
        }

        public void Update()
        {
        }

        private void SelectObject(InputAction.CallbackContext obj)
        {
            _container.Resolve<SelectObject>().Execute();
        }
    }
}