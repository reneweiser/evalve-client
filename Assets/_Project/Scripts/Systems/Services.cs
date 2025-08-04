using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evalve.Systems
{
    public static class Services
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void Register<T>(T service)
        {
            if (_services.ContainsKey(typeof(T)))
            {
                Debug.LogWarning($"Service already registered: {typeof(T)}");
            }
            
            _services[typeof(T)] = service;
        }

        public static T Get<T>()
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            
            Debug.LogWarning($"Service not registered: {typeof(T)}");
            return default;
        }
    }
}
