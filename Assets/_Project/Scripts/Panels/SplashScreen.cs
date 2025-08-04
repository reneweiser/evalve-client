using System;
using UnityEngine;
using UnityEngine.UI;

namespace Evalve.Panels
{
    public class SplashScreen : UiPanel
    {
        public event Action Confirmed;
        
        [SerializeField] private Button _confirmButton;

        private void OnEnable() => _confirmButton.onClick.AddListener(() => Confirmed?.Invoke());

        private void OnDisable() => _confirmButton.onClick.RemoveAllListeners();
    }
}