using Evalve.Contracts;
using UnityEngine;

namespace Evalve.App
{
    public class SceneObjectBehaviour : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Transform _posePrefab;
        [Header("Dependencies")]
        [SerializeField] private SceneObjectHandle _handle;
        [SerializeField] private Material _idle;
        [SerializeField] private Material _dragging;
        [SerializeField] private Material _selected;
        
        private string _id;
        public string ScreenshotPath { get; set; }

        public void SetId(string id) => _id = id;

        public string GetId() => _id;

        public void SetInteraction(InteractionType type)
        {
            GetComponentInChildren<SceneObjectHandle>().SetInteraction(type);
        }
    }
}