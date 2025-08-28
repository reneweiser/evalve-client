using System.Threading.Tasks;
using Evalve.App.States;
using Evalve.App.States.CreatingSessions;
using Evalve.Contracts;
using UnityEngine.EventSystems;
using VContainer;

namespace Evalve.App.Commands
{
    public class SelectObject : ICommand
    {
        private readonly SceneCursor _cursor;
        private readonly EventSystem _eventSystem;
        private readonly StateMachine _stateMachine;
        private readonly IObjectManager _objectManager;
        private readonly IObjectResolver _container;

        public SelectObject(SceneCursor cursor,
            EventSystem eventSystem,
            StateMachine stateMachine,
            IObjectManager objectManager,
            IObjectResolver container)
        {
            _cursor = cursor;
            _eventSystem = eventSystem;
            _stateMachine = stateMachine;
            _objectManager = objectManager;
            _container = container;
        }
        public Task Execute()
        {
            if (!_cursor.IsValid)
            {
                if (_eventSystem.IsPointerOverGameObject())
                    return Task.CompletedTask;
                
                _objectManager.SelectObject(null);
                _stateMachine.ChangeState(_container.Resolve<Idle>());
                return Task.CompletedTask;
            }

            var interactable = _cursor.Data.collider.GetComponent<IInteractable>();
            
            if (interactable == null)
                return Task.CompletedTask;
            
            _objectManager.SelectObject(interactable.GetId());
            _stateMachine.ChangeState(_container.Resolve<EditingObject>());
            
            return Task.CompletedTask;
        }
    }
}