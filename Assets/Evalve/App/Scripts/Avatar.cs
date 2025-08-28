using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Evalve.App
{
    public class Avatar : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private FlyingMovement _flyingMovement;
        [SerializeField] private CameraRotation _cameraRotation;

        [Inject]
        public void Initialize(InputActionAsset input)
        {
            _flyingMovement.SetInput(input);
            _cameraRotation.SetInput(input);
        }
        
        public void Teleport(Vector3 position, float rotation)
        {
            _flyingMovement.enabled = false;
            _cameraRotation.enabled = false;
            transform.position = position;
            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            _flyingMovement.enabled = true;
            _cameraRotation.enabled = true;
            _cameraRotation.DisableLook();
        }

        public Camera GetCamera()
        {
            return _camera;
        }
    }
}