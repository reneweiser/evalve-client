using System;
using Evalve.Contracts;
using UnityEngine;

namespace Evalve.App
{
    public class SceneObjectPose : MonoBehaviour
    {
        public string role;
        public string id;
        public string objectId;

        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _selectedMaterial;
        [SerializeField] private Material _idleMaterial;

        public void SetInteraction(InteractionType type)
        {
            _renderer.material = type switch
            {
                InteractionType.Idle => _idleMaterial,
                InteractionType.Selected or InteractionType.Dragging => _selectedMaterial,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}