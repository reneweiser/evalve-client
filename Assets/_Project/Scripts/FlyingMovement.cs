using Evalve.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Evalve
{
    public class FlyingMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10f;
        
        private Camera _playerCamera;
        private InputActionAsset _inputActions;
        private Vector2 _movementInput;
        private float _heightInput;
        private float _sprintCoefficient = 1f;

        private void Start()
        {
            _playerCamera = Services.Get<Camera>();
            _inputActions = Services.Get<InputActionAsset>();
            _inputActions["Move"].performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
            _inputActions["Move"].canceled += ctx => _movementInput = Vector2.zero;
            _inputActions["Elevation"].performed += ctx => _heightInput = ctx.ReadValue<float>();
            _inputActions["Elevation"].canceled += ctx => _heightInput = 0.0f;
            _inputActions["Sprint"].performed += ctx => _sprintCoefficient = 3f;
            _inputActions["Sprint"].canceled += ctx => _sprintCoefficient = 1f;
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
        
            transform.position += moveDirection.normalized * _moveSpeed * _sprintCoefficient * Time.deltaTime;
        }
    }
}