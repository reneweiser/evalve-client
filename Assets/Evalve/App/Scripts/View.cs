using System;
using System.Collections.Generic;
using Evalve.App.Ui.Elements;
using UnityEngine;

namespace Evalve.App
{
    public abstract class View<TModel> : MonoBehaviour
        where TModel : Model
    {
        [SerializeField] protected Column _panel;
        
        private readonly Dictionary<Type, Delegate> _eventMap = new();
        
        public void Subscribe<T>(Action<T> handler)
        {
            if (!_eventMap.ContainsKey(typeof(T)))
                _eventMap[typeof(T)] = null;

            _eventMap[typeof(T)] = (Action<T>)_eventMap[typeof(T)] + handler;
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            if (_eventMap.ContainsKey(typeof(T)))
                _eventMap[typeof(T)] = (Action<T>)_eventMap[typeof(T)] - handler;
        }

        protected void Raise<T>(T args)
        {
            if (_eventMap.TryGetValue(typeof(T), out var del))
                (del as Action<T>)?.Invoke(args);
        }
        
        public void SetVisible(bool isVisible) => gameObject.SetActive(isVisible);
        
        public virtual void Initialize(TModel model) { }
            
        public virtual void Refresh(TModel model) { }
        
        public virtual void Tick(TModel model) { }
        
        public virtual void Cleanup() { }
    }
}