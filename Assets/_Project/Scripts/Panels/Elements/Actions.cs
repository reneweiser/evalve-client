using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evalve.Panels.Elements
{
    public class Actions : MonoBehaviour
    {
        [SerializeField] private MenuButton _prefab;
        [SerializeField] private Transform _container;

        private readonly Dictionary<string, ActionItem> _actionItems = new();

        public void AddAction(string label, Action action)
        {
            var button = Instantiate(_prefab, _container);
            
            _actionItems[label] = new ActionItem(label, action, button);
        }

        public void RemoveAction(string label)
        {
            _actionItems[label].Clear();
            _actionItems.Remove(label);
        }
    }
}