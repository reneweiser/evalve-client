using System.Collections.Generic;
using Evalve.Client;
using UnityEngine;

namespace Evalve.App
{
    public interface IObjectManager
    {
        List<SceneObjectBehaviour> GetObjects();
        void CreateObject(SceneObject sceneObject);
        SceneObjectBehaviour CreateNewObject();
        SceneObjectBehaviour GetObject(string objectId);
        void DeleteObject(string objectId);
        void RenameObject(string objectId, string newName);
        void SelectObject(string objectId);
        string GetSelectedObjectId();
        void StartDragObject(string objectId);
        void StopDragObject(string objectId);
        
        List<SceneObjectPose> GetPoses(string objectId);
        SceneObjectPose GetPose(string objectId, string poseId);
        void DeletePose(string objectId, string poseId);
        void SelectPose(string objectId, string poseId);
        void CreatePoseAt(string objectId, Vector3 position, Vector3 rotation);
        string GetSelectedPoseId();
        void MovePoseAt(string sObjectId, string poseId, Vector3 position, Vector3 rotation);
        void SetPoseRole(string objectId, string poseId, string newRole);
        void Cleanup();
    }
}