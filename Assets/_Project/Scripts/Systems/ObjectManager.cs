using System;
using System.Collections.Generic;
using UnityEngine;
using SceneObject = Evalve.SceneObjects.SceneObject;

namespace Evalve.Systems
{
    public class ObjectManager : MonoBehaviour
    {
        public event Action<SceneObject> Created;
        public event Action<SceneObject> Updated;
        public event Action<SceneObject> Deleted;
        
        private readonly List<SceneObject> _sceneObjects = new();
        
        public void Create(SceneObject sceneObject)
        {
            _sceneObjects.Add(sceneObject);
            Created?.Invoke(sceneObject);
            sceneObject.transform.SetParent(transform);
        }

        public void Delete(SceneObject sceneObject)
        {
            Deleted?.Invoke(sceneObject);
            _sceneObjects.Remove(sceneObject);
            Destroy(sceneObject.gameObject);
        }

        public void Refresh(SceneObject sceneObject)
        {
            Updated?.Invoke(sceneObject);
        }
    }
}