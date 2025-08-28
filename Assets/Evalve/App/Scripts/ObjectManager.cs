using System.Collections.Generic;
using System.Linq;
using Evalve.Client;
using Evalve.Contracts;
using UnityEngine;
using Pose = Evalve.Client.Pose;

namespace Evalve.App
{
    public class ObjectManager : IObjectManager
    {
        private readonly PrefabRegistry _prefabRegistry;
        private readonly Dictionary<string, SceneObjectBehaviour> _sObjects = new();
        private readonly Dictionary<string, List<SceneObjectPose>> _soProperties = new();
        private string _selectedSceneObjectId;
        private string _selectedPoseId;

        public ObjectManager(PrefabRegistry prefabRegistry)
        {
            _prefabRegistry = prefabRegistry;
        }

        public List<SceneObjectBehaviour> GetObjects()
        {
            return _sObjects.Values.ToList();
        }

        public void CreateObject(SceneObject sceneObject)
        {
            var sObject = sceneObject.ToBehaviour(_prefabRegistry).GetComponent<SceneObjectBehaviour>();
            
            _sObjects.Add(sObject.GetId(), sObject);
            _soProperties[sObject.GetId()] = sObject.GetComponentsInChildren<SceneObjectPose>()
                .ToList();
        }

        public SceneObjectBehaviour GetObject(string objectId)
        {
            return _sObjects[objectId];
        }

        public void DeleteObject(string objectId)
        {
            Object.Destroy(_sObjects[objectId].gameObject);
            _sObjects.Remove(objectId);
            _soProperties.Remove(objectId);
        }

        public void RenameObject(string objectId, string newName)
        {
            var sObject = _sObjects[objectId];
            sObject.name = newName;
        }

        public void SelectObject(string objectId)
        {
            _selectedSceneObjectId = objectId;
            
            foreach (var sObject in _sObjects.Values)
            {
                var isSelected = objectId == sObject.GetId()
                    ? InteractionType.Selected
                    : InteractionType.Idle;
                
                sObject.SetInteraction(isSelected);
            }
        }

        public string GetSelectedObjectId()
        {
            return _selectedSceneObjectId;
        }

        public void StartDragObject(string objectId)
        {
            var sObject = _sObjects[objectId];
            sObject.SetInteraction(InteractionType.Dragging);
        }

        public void StopDragObject(string objectId)
        {
            var sObject = _sObjects[objectId];
            sObject.SetInteraction(InteractionType.Selected);
        }

        public List<SceneObjectPose> GetPoses(string objectId)
        {
            return _soProperties[objectId];
        }

        public SceneObjectPose GetPose(string objectId, string poseId)
        {
            return _soProperties[objectId].First(i => i.id == poseId);
        }

        public void DeletePose(string objectId, string poseId)
        {
            var pose = GetPose(objectId, poseId);
            _selectedPoseId = null;
            _soProperties[objectId].Remove(pose);
            
            Object.Destroy(pose.gameObject);
        }

        public void SelectPose(string objectId, string poseId)
        {
            _selectedPoseId = poseId;
            var sObject = _sObjects[objectId];

            foreach (var sObjectPose in sObject.GetComponentsInChildren<SceneObjectPose>())
            {
                var isSelected = poseId == null
                    ? InteractionType.Idle
                    : poseId == sObjectPose.id ? InteractionType.Selected : InteractionType.Idle;
                
                sObjectPose.SetInteraction(isSelected);
            }
        }

        public void CreatePoseAt(string objectId, Vector3 position, Vector3 rotation)
        {
            var selectedObject = _sObjects[objectId];

            var pose = new Pose
            {
                Position = position.ToVector(),
                Rotation = rotation.ToVector(),
                ObjectId = objectId,
                Role = "Default"
            }
                .ToProperty(_prefabRegistry)
                .GetComponent<SceneObjectPose>();

            pose.transform.SetParent(selectedObject.transform);
            
            _soProperties[objectId].Add(pose);
        }

        public string GetSelectedPoseId()
        {
            return _selectedPoseId;
        }

        public void MovePoseAt(string sObjectId, string poseId, Vector3 position, Vector3 rotation)
        {
            var sObject = _sObjects[sObjectId];
            var pose = sObject
                .GetComponentsInChildren<SceneObjectPose>()
                .First(i => i.id == poseId);
            
            pose.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
        }

        public void SetPoseRole(string objectId, string poseId, string newRole)
        {
            var sObject = _sObjects[objectId];
            var pose = sObject
                .GetComponentsInChildren<SceneObjectPose>()
                .First(i => i.id == poseId);
            
            pose.role = newRole;
        }
    }
}