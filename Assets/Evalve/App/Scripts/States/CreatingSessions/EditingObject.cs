using Evalve.App.Commands;
using Evalve.Contracts;
using UnityEngine.InputSystem;
using VContainer;

namespace Evalve.App.States.CreatingSessions
{
    public class EditingObject : IState
    {
        private readonly IObjectManager _objectManager;
        private readonly IObjectResolver _container;
        private readonly StateMachine _stateMachine;
        private readonly EditingObjectPresenter _presenter;
        private readonly InputActionAsset _input;
        
        private SceneObjectBehaviour _selectedObject;

        public EditingObject(IObjectManager objectManager,
            IObjectResolver container,
            StateMachine stateMachine,
            EditingObjectPresenter presenter,
            InputActionAsset input)
        {
            _objectManager = objectManager;
            _container = container;
            _stateMachine = stateMachine;
            _presenter = presenter;
            _input = input;
        }
        
        public void Enter()
        {
            _input["Use"].canceled += SelectObject;
            _input["CancelUse"].started += Back;
            
            _presenter.Initialize();
            _presenter.SetViewVisible(true);
        }

        public void Exit()
        {
            _input["Use"].canceled -= SelectObject;
            _input["CancelUse"].started -= Back;
            
            _presenter.SetViewVisible(false);
            _presenter.Cleanup();
        }

        public void Update()
        {
            _presenter.Tick();
        }
        
        private void SelectObject(InputAction.CallbackContext context)
        {
            _container.Resolve<SelectObject>().Execute();
        }

        private void Back(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState(_container.Resolve<Idle>());
        }
    }
}