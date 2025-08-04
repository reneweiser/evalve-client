using UnityEngine;

namespace Evalve.SceneObjects
{
    public class SceneObject : MonoBehaviour
    {
        [SerializeField] Handle _handle;
        [SerializeField] Checkpoint _checkpointPrefab;
        [SerializeField] Pose _posePrefab;
        
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
    }
}