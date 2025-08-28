using System;
using UnityEngine;

namespace Evalve.App.Ui.Elements
{
    public class Button : Element
    {
        public event Action Clicked;
        [SerializeField] private UnityEngine.UI.Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(() => Clicked?.Invoke());
        }
    }
}