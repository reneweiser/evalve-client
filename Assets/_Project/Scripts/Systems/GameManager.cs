using Evalve.Panels;
using Evalve.SceneObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using SceneObject = Evalve.Panels.SceneObject;

namespace Evalve.Systems
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;
        [SerializeField] private SceneCursor _sceneCursor;
        [SerializeField] private Spawner spawner;
        [SerializeField] private SceneObject _ui;
        [SerializeField] private Avatar _avatar;
        [SerializeField] private Camera _camera;
        [SerializeField] private Factory factory;
        [SerializeField] private UiStateMachine _uiStateMachine;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private AssetManager _assetManager;
        
        private void Awake()
        {
            Services.Register(_avatar);
            Services.Register(_camera);
            Services.Register(_eventSystem);
            Services.Register(_inputActions);
            Services.Register(_sceneCursor);
            Services.Register(spawner);
            Services.Register(factory);
            Services.Register(_ui);
            Services.Register(_uiStateMachine);
            Services.Register(_assetManager);
        }
        private void Start()
        {
            // _uiStateMachine.ChangeState<States.SelectingTool>();
            _uiStateMachine.ChangeState<States.Setup>();
            // _uiStateMachine.ChangeState<States.Test>();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }
    }
}