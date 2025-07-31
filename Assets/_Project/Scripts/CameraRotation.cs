using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Evalve
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 100f;
        [SerializeField] private InputActionAsset _inputActions;
        
        private Vector2 lookInput;
        private float pitch = 0f;
        
        private void Awake()
        {
            _inputActions["Look"].performed += ctx => lookInput = ctx.ReadValue<Vector2>();
            _inputActions["Look"].canceled += ctx => lookInput = Vector2.zero;
            _inputActions["EnableLook"].performed += EnableLook;
            _inputActions["EnableLook"].canceled += DisableLook;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _inputActions["Look"].Disable();
            _inputActions["Move"].Disable();
            _inputActions["Elevation"].Disable();
        }

        private void DisableLook(InputAction.CallbackContext ctx)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _inputActions["Look"].Disable();
            _inputActions["Move"].Disable();
            _inputActions["Elevation"].Disable();
        }

        private void EnableLook(InputAction.CallbackContext ctx)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _inputActions["Look"].Enable();
            _inputActions["Move"].Enable();
            _inputActions["Elevation"].Enable();
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
            HandleRotation();
        }

        private void HandleRotation()
        {
            var mouseX = lookInput.x * _rotationSpeed * Time.deltaTime;
            var mouseY = lookInput.y * _rotationSpeed * Time.deltaTime;

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            transform.localRotation = Quaternion.Euler(pitch, transform.localRotation.eulerAngles.y, 0f);
        
            transform.parent.Rotate(Vector3.up * mouseX);
        }
    }
}