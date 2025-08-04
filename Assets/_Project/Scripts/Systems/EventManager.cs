using System;
using UnityEngine;

namespace Evalve.Systems
{
    public class EventManager : MonoBehaviour
    {
        public static event Action<object> OnGameEvent;

        public static void RaiseEvent(object data) => OnGameEvent?.Invoke(data);
    }
}