using UnityEngine;

namespace Evalve.SceneObjects
{
    public class Handle : MonoBehaviour
    {
        [SerializeField] private Material _idle;
        [SerializeField] private Material _selected;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Collider _collider;

        public void SetIsSelected(bool isSelected)
        {
            _renderer.material = isSelected ? _selected : _idle;
        }

        public void SetIsDragging(bool isDragging)
        {
            _collider.enabled = !isDragging;
        }
    }
}