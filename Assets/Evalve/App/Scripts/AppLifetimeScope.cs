using System;
using System.Linq;
using Evalve.App.States;
using Evalve.App.States.CreatingSessions;
using Evalve.App.Ui.Elements;
using Evalve.Client;
using Evalve.Contracts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;
using ILogger = Evalve.Contracts.ILogger;

namespace Evalve.App
{
    public class AppLifetimeScope : LifetimeScope
    {
        [Space]
        [SerializeField] private Avatar _avatarPrefab;
        [SerializeField] private SceneCursor _sceneCursorPrefab;
        [SerializeField] private ScreenshotCamera _screenshotCameraPrefab;
        [SerializeField] private Notifications _notificationsPrefab;
        
        [Space]
        [SerializeField] private InputActionAsset _inputAsset;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private PrefabRegistry _prefabRegistry;
        
        [Space]
        [Header("Views")]
        [SerializeField] private EditingObjectView _editingObjectView;
        [SerializeField] private MovingObjectView _movingObjectView;
        [SerializeField] private EditingPoseView _editingPoseView;
        [SerializeField] private SelectingResourcesView _selectingResourcesView;
        [SerializeField] private CreatingSceneView _creatingSceneView;
        [SerializeField] private CreatingSessionView _creatingSessionView;
        [SerializeField] private IdleView _idleView;

        [Space] [Header("Views")] [SerializeField]
        private string _baseUrl;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrap>();
            
            builder.RegisterInstance(_inputAsset);
            builder.RegisterInstance(_mainCamera);
            builder.RegisterInstance(_prefabRegistry);
            
            builder.RegisterComponentInNewPrefab(_creatingSessionView, Lifetime.Transient);
            builder.RegisterComponentInNewPrefab(_selectingResourcesView, Lifetime.Transient);
            builder.RegisterComponentInNewPrefab(_creatingSceneView, Lifetime.Transient);
            builder.RegisterComponentInNewPrefab(_editingObjectView, Lifetime.Transient);
            builder.RegisterComponentInNewPrefab(_movingObjectView, Lifetime.Transient);
            builder.RegisterComponentInNewPrefab(_editingPoseView, Lifetime.Transient);
            builder.RegisterComponentInNewPrefab(_idleView, Lifetime.Transient);
            
            builder.Register<CreatingSessionPresenter>(Lifetime.Transient);
            builder.Register<SelectingResourcesPresenter>(Lifetime.Transient);
            builder.Register<CreatingScenePresenter>(Lifetime.Transient);
            builder.Register<EditingObjectPresenter>(Lifetime.Transient);
            builder.Register<MovingObjectPresenter>(Lifetime.Transient);
            builder.Register<EditingPosePresenter>(Lifetime.Transient);
            builder.Register<IdlePresenter>(Lifetime.Transient);
            
            builder.RegisterComponentInNewPrefab(_avatarPrefab, Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(_sceneCursorPrefab, Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(_screenshotCameraPrefab, Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(_notificationsPrefab, Lifetime.Singleton);
            
            builder.Register<StateMachine>(Lifetime.Singleton);
            builder.Register<Session>(Lifetime.Singleton);
            builder.Register<IObjectManager, ObjectManager>(Lifetime.Singleton);
            builder.Register<ISessionManager, SessionManager>(Lifetime.Singleton);
            builder.Register<IAssetManager, AssetManager>(Lifetime.Singleton);
            builder.Register<AssetBundleLoader>(Lifetime.Singleton);
            builder.Register<ILogger, EvalveLogger>(Lifetime.Singleton);
            
            builder.Register<IConnection, Connection>(Lifetime.Singleton).WithParameter("baseUrl", _baseUrl);
            
            builder.RegisterComponentInHierarchy<EventSystem>();
            
            RegisterAssignableFrom<Model>(builder, Lifetime.Transient);
            RegisterAssignableFrom<IState>(builder, Lifetime.Transient);
            RegisterAssignableFrom<ICommand>(builder, Lifetime.Transient);
        }

        private void RegisterAssignableFrom<T>(IContainerBuilder builder, Lifetime lifetime)
        {
            var commandType = typeof(T);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var commandImplementations = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => commandType.IsAssignableFrom(t) 
                            && t.IsClass 
                            && !t.IsAbstract);

            foreach (var impl in commandImplementations)
            {
                builder.Register(impl, lifetime);
            }
        }
    }
}