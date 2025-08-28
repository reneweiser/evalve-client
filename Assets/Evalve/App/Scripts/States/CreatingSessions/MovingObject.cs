using Evalve.App.Commands;
using Evalve.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Evalve.App.States.CreatingSessions
{
    public class MovingObject : IState
    {
        private readonly MovingObjectPresenter _presenter;
        private readonly InputActionAsset _input;
        private readonly SceneCursor _cursor;
        private readonly IObjectManager _objectManager;
        private readonly IObjectResolver _container;
        private readonly StateMachine _stateMachine;

        public MovingObject(MovingObjectPresenter presenter,
            InputActionAsset input,
            SceneCursor cursor,
            IObjectManager objectManager,
            IObjectResolver container,
            StateMachine stateMachine)
        {
            _presenter = presenter;
            _input = input;
            _cursor = cursor;
            _objectManager = objectManager;
            _container = container;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _input["Use"].canceled += ConfirmPosition;
            
            _presenter.Initialize();

            _objectManager.StartDragObject(_objectManager.GetSelectedObjectId());
        }

        public void Exit()
        {
            _input["Use"].canceled -= ConfirmPosition;
            
            _objectManager.StopDragObject(_objectManager.GetSelectedObjectId());
            _container.Resolve<UpdateSelectedSceneObject>();
            
            _presenter.Cleanup();
        }

        public void Update()
        {
            var sObject = _objectManager.GetObject(_objectManager.GetSelectedObjectId());
            sObject.transform.position = _cursor.Point;
            
            _presenter.Tick();
        }

        private void ConfirmPosition(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState(_container.Resolve<EditingObject>());
        }
    }
}