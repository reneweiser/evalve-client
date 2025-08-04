using UnityEngine;

namespace Evalve
{
    public class Avatar : MonoBehaviour
    {
        [SerializeField] private FlyingMovement _flyingMovement;
        [SerializeField] private CameraRotation _cameraRotation;
        
        public void Teleport(Vector3 position, float rotation)
        {
            _flyingMovement.enabled = false;
            _cameraRotation.enabled = false;
            transform.position = position;
            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            _flyingMovement.enabled = true;
            _cameraRotation.enabled = true;
        }
    }
}