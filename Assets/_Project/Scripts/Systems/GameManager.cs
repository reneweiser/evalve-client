using Evalve.Client;
using Evalve.Panels;
using Evalve.SceneObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Evalve.Systems
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;
        [SerializeField] private SceneCursor _sceneCursor;
        [SerializeField] private Ui _ui;
        [SerializeField] private Avatar _avatar;
        [SerializeField] private Camera _camera;
        [SerializeField] private Factory factory;
        [SerializeField] private UiStateMachine _uiStateMachine;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private AssetManager _assetManager;
        [SerializeField] private ObjectManager _objectManager;
        
        private void Awake()
        {
            Services.Register(_avatar);
            Services.Register(_camera);
            Services.Register(_eventSystem);
            Services.Register(_inputActions);
            Services.Register(_sceneCursor);
            Services.Register(factory);
            Services.Register(_ui);
            Services.Register(_uiStateMachine);
            Services.Register(_assetManager);
            Services.Register(new Connection("http://localhost/api/v1", "1|mTURpfnVVVgego7RMLyVPB2s9WI8rZ8kdI39Nszr67aa7271"));
            Services.Register(new CommandBus());
            Services.Register(new SceneObjectSerializer());
            Services.Register(_objectManager);
        }
        private void Start()
        {
            // _uiStateMachine.ChangeState<States.Idle>();
            // _uiStateMachine.ChangeState<States.Setup>();
            _uiStateMachine.ChangeState(new States.CreateSession());
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