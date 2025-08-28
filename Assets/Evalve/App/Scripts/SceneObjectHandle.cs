using System;
using Evalve.Contracts;
using UnityEngine;

namespace Evalve.App
{
    [RequireComponent(typeof(Collider))]
    public class SceneObjectHandle : MonoBehaviour, IInteractable
    {
        [SerializeField] private Material _idle;
        [SerializeField] private Material _dragging;
        [SerializeField] private Material _selected;
        
        private Collider _collider;
        private Renderer _renderer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _renderer = GetComponent<Renderer>();
        }

        public string GetId()
        {
            return GetComponentInParent<SceneObjectBehaviour>().GetId();
        }

        public void SetInteraction(InteractionType type)
        {
            switch (type)
            {
                case InteractionType.Idle:
                    _collider.enabled = true;
                    _renderer.material = _idle;
                    break;
                case InteractionType.Selected:
                    _collider.enabled = true;
                    _renderer.material = _selected;
                    break;
                case InteractionType.Dragging:
                    _collider.enabled = false;
                    _renderer.material = _dragging;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}