using System.Collections.Generic;
using UnityEngine;

namespace Evalve.SceneObjects
{
    public class SceneObject : MonoBehaviour
    {
        public Client.SceneObject Data { get; private set; }
        
        [SerializeField] Handle _handle;
        [SerializeField] Checkpoint _checkpointPrefab;
        [SerializeField] Pose _posePrefab;
        private bool _isActive;

        private void Awake()
        {
            gameObject.SetActive(_isActive);
        }

        public void AddPose(string role, Vector3 position, Quaternion rotation)
        {
            var obj = Instantiate(_posePrefab, transform);
            obj.name = role;
            obj.transform.SetPositionAndRotation(position, rotation);
        }

        public void SetIsSelected(bool isSelected)
        {
            _handle.SetIsSelected(isSelected);
        }

        public void SetIsDragging(bool isDragging)
        {
            _handle.SetIsDragging(isDragging);
        }

        public IEnumerable<Pose> GetPoses()
        {
            return GetComponentsInChildren<Pose>();
        }

        public void SetIsActive(bool isActive)
        {
            _isActive = isActive;
            gameObject.SetActive(_isActive);
        }

        public void Toggle()
        {
            _isActive = !_isActive;
            gameObject.SetActive(_isActive);
        }

        public void SetData(Client.SceneObject data)
        {
            Data = data;
        }
    }
}