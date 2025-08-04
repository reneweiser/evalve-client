using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Evalve.Panels
{
    public class SceneObject : MonoBehaviour
    {
        private Dictionary<Type, UiPanel> _instances;

        private void Awake()
        {
            _instances = GetComponentsInChildren<UiPanel>(true)
                .ToDictionary(
                    component => component.GetType(),
                    component => component
                );
        }

        public T Show<T>() where T : MonoBehaviour
        {
            foreach (var item in _instances)
            {
                item.Value.SetIsActive(item.Key == typeof(T));
            }

            if (!_instances.TryGetValue(typeof(T), out var instance))
            {
                throw new InvalidOperationException("Could not find instance of type " + typeof(T));
            }
            
            return instance as T;
        }
    }
}