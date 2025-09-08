using Evalve.App.Commands;
using Evalve.Contracts;
using UnityEngine.InputSystem;
using VContainer;

namespace Evalve.App.States
{
    public class Idle : IState
    {
        private readonly IdlePresenter _presenter;
        private readonly IObjectResolver _container;
        private readonly InputActionAsset _input;

        public Idle(IdlePresenter presenter, IObjectResolver container, InputActionAsset input) {
            _presenter = presenter;
            _container = container;
            _input = input;
        }
        
        public void Enter()
        {
            _input["Use"].canceled += SelectObject;
            _presenter.Initialize();
        }

        public void Exit()
        {
            _input["Use"].canceled -= SelectObject;
            _presenter.Cleanup();
        }

        public void Update()
        {
            _presenter.Tick();
        }

        private void SelectObject(InputAction.CallbackContext obj)
        {
            _container.Resolve<SelectObject>().Execute();
        }
    }
}