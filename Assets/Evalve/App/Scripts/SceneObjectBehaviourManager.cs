using System.Collections.Generic;
using System.Linq;
using Evalve.Client;
using UnityEngine;

namespace Evalve.App
{
    public class SceneObjectBehaviourManager : MonoBehaviour
    {
        public SceneObjectBehaviour SelectedObject { get; private set; }
        public SceneObjectPose SelectedPose { get; private set; }
        
        [SerializeField] private PrefabRegistry _prefabRegistry;
        [SerializeField] private UnityEngine.Transform _container;
        
        private readonly Dictionary<string, SceneObjectBehaviour> _behaviours = new();
        
        public SceneObjectBehaviour Add(SceneObject sceneObject)
        {
            var behaviour = sceneObject.ToBehaviour(_prefabRegistry);
            var so = behaviour.GetComponent<SceneObjectBehaviour>();

            so.transform.SetParent(_container);
            _behaviours.Add(sceneObject.Id, so);
            
            return so;
        }

        public void Remove(string id)
        {
            if (!_behaviours.TryGetValue(id, out var behaviour))
                return;
            
            Destroy(behaviour.gameObject);
            _behaviours.Remove(id);
        }

        public void MarkSelected(string id)
        {
            if (!_behaviours.TryGetValue(id, out var behaviour))
                return;

            SelectedObject = behaviour;
        }

        public void SelectPose(string id)
        {
            if (SelectedObject == null)
                return;

            SelectedPose = SelectedObject.GetComponentsInChildren<SceneObjectPose>()
                .First(pose => pose.name == id);
        }

        public void MovePose(Vector position, Vector rotation)
        {
            throw new System.NotImplementedException();
        }
    }
}