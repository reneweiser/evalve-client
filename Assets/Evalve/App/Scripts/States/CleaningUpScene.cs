using Evalve.App.States.CreatingSessions;
using Evalve.Contracts;
using UnityEngine;
using VContainer;

namespace Evalve.App.States
{
    public class CleaningUpScene : IState
    {
        private readonly IAssetManager _assetManager;
        private readonly IObjectManager _objectManager;
        private readonly IObjectResolver _container;
        private readonly Camera _camera;
        private readonly Avatar _avatar;
        private readonly Session _session;
        private readonly ISessionManager _sessionManager;
        private readonly StateMachine _stateMachine;

        public CleaningUpScene(IAssetManager assetManager,
            IObjectManager objectManager,
            IObjectResolver container,
            Camera camera,
            Avatar avatar,
            Session session,
            ISessionManager sessionManager,
            StateMachine stateMachine)
        {
            _assetManager = assetManager;
            _objectManager = objectManager;
            _container = container;
            _camera = camera;
            _avatar = avatar;
            _session = session;
            _sessionManager = sessionManager;
            _stateMachine = stateMachine;
        }
        
        public async void Enter()
        {
            _camera.gameObject.SetActive(true);
            _avatar.gameObject.SetActive(false);
            _assetManager.Cleanup();
            _objectManager.Cleanup();
            await _sessionManager.PushObjects();
            _session.UserSelectedAssets.Clear();
            _session.UserSelectedObjects.Clear();
            _stateMachine.ChangeState(_container.Resolve<SelectingResources>());
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }
}