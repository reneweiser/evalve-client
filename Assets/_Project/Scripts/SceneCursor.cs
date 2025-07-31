using UnityEngine;
using UnityEngine.InputSystem;

namespace Evalve
{
    public class SceneCursor : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _sceneCursor;

        public RaycastHit Data;
        
        private void FixedUpdate()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var ray = _playerCamera.ScreenPointToRay(mousePosition);
            
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, _layerMask))
                return;
            
            Data = hit;
            _sceneCursor.transform.position = hit.point;
        }
    }
}