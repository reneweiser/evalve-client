using System.Threading.Tasks;
using Evalve.App.Commands;
using Evalve.Contracts;
using UnityEngine;
using VContainer;
using LogType = Evalve.Contracts.LogType;

namespace Evalve.App.States.CreatingSessions
{
    public class CreatingScene : IState
    {
        private readonly CreatingScenePresenter _presenter;
        private readonly Contracts.ILogger _logger;
        private readonly Avatar _avatar;
        private readonly Camera _camera;
        private readonly SceneCursor _cursor;
        private readonly StateMachine _stateMachine;
        private readonly IObjectResolver _container;

        public CreatingScene(CreatingScenePresenter presenter,
            Contracts.ILogger logger,
            Avatar avatar,
            Camera camera,
            SceneCursor cursor,
            StateMachine stateMachine,
            IObjectResolver container)
        {
            _presenter = presenter;
            _logger = logger;
            _avatar = avatar;
            _camera = camera;
            _cursor = cursor;
            _stateMachine = stateMachine;
            _container = container;
        }
        
        public async void Enter()
        {
            _camera.gameObject.SetActive(true);
            _avatar.gameObject.SetActive(false);
            _presenter.Initialize();
            _presenter.SetViewVisible(true);
            _logger.Log("Creating scene. Hold on!", LogType.Message);
            
            await _container.Resolve<DownloadSelectedAssets>().Execute();
            await _container.Resolve<SpawnSelectedAssets>().Execute();
            await _container.Resolve<SpawnSelectedObjects>().Execute();
            
            _logger.Log("Successfully created scene. Have fun!", LogType.Success);
            await Task.Delay(1000);
            
            _stateMachine.ChangeState(_container.Resolve<Idle>());
        }

        public void Exit()
        {
            _camera.gameObject.SetActive(false);
            _avatar.gameObject.SetActive(true);
            
            _presenter.SetViewVisible(false);
            _presenter.Cleanup();
        }

        public void Update()
        {
            _presenter.Tick();
        }
    }
}