using System;
using Evalve.Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Evalve
{
    public class SceneCursor : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _sceneCursor;

        private Camera _playerCamera;
        public RaycastHit Data;
        public bool IsValid;
        private EventSystem _eventSystem;

        private void Start()
        {
            _eventSystem = Services.Get<EventSystem>();
            _playerCamera = Services.Get<Camera>();
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
                _sceneCursor.SetActive(false);
                Data = new RaycastHit();
                return;
            }
            
            IsValid = true;
            _sceneCursor.SetActive(true);
            Data = hit;
            _sceneCursor.transform.position = hit.point;
        }
    }
}