using UnityEngine;
using UnityEngine.InputSystem;

namespace Evalve
{
    public class FlyingMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private InputActionAsset _inputActions;
    
        private Vector2 _movementInput;
        private float _heightInput;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputActions["Move"].performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
            _inputActions["Move"].canceled += ctx => _movementInput = Vector2.zero;
            _inputActions["Elevation"].performed += ctx => _heightInput = ctx.ReadValue<float>();
            _inputActions["Elevation"].canceled += ctx => _heightInput = 0.0f;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            var verticalInput = _movementInput.y;
            var horizontalInput = _movementInput.x;

            var moveDirection = _playerCamera.transform.forward * verticalInput
                                + _playerCamera.transform.right * horizontalInput
                                + transform.up * _heightInput;
        
            transform.position += moveDirection.normalized * _moveSpeed * Time.deltaTime;
        }
    }
}