using System;
using Evalve.Panels.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Evalve.Panels
{
    public class SessionConfigurator : UiPanel
    {
        public event Action Confirm;

        [SerializeField] private Button _button;
        [SerializeField] private Actions _assets;
        [SerializeField] private Actions _objects;

        private void Awake()
        {
            _button.onClick.AddListener(() => Confirm?.Invoke());
        }

        public void AddAsset(string label, Action action)
        {
            _assets.AddAction(label, action);
        }
        
        public void AddObject(string label, Action action)
        {
            _objects.AddAction(label, action);
        }
    }
}