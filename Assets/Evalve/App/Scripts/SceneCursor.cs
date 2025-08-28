using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer;

namespace Evalve.App
{
    public class SceneCursor : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _sceneCursor;
        [SerializeField] private float _invalidPointDistance = 10f;

        private Camera _playerCamera;
        public Vector3 Point { get; private set; }
        public RaycastHit Data { get; private set; }
        public bool IsValid;
        private EventSystem _eventSystem;

        [Inject]
        private void Construct(EventSystem eventSystem, Avatar avatar)
        {
            _eventSystem = eventSystem;
            _playerCamera = avatar.GetCamera();
        }

        private void FixedUpdate()
        {
            if (_eventSystem.IsPointerOverGameObject())
            {
                IsValid = false;
                return;
            }
            
            var mousePosition = Mouse.current.position.ReadValue();
            var ray = _playerCamera.ScreenPointToRay(mousePosition);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, _layerMask))
            {
                IsValid = false;
                Point = ray.GetPoint(_invalidPointDistance);
                Data = new RaycastHit();
                return;
            }
            
            IsValid = true;
            Point = hit.point;
            Data = hit;
        }

    }
}