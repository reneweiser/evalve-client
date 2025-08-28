using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evalve.App.Ui.Elements
{
    public class Toggle : Element
    {
        public event Action<bool> InputChanged;
        
        [SerializeField] private UnityEngine.UI.Button _button;
        [SerializeField] private Image _image;
        
        private bool _isActive;

        public void SetValue(bool value)
        {
            _isActive = value;
            _image.gameObject.SetActive(_isActive);
        }

        private void Awake()
        {
            _image.gameObject.SetActive(_isActive);
            _button.onClick.AddListener(ToggleInput);
        }

        private void ToggleInput()
        {
            _isActive = !_isActive;
            _image.gameObject.SetActive(_isActive);
            InputChanged?.Invoke(_isActive);
        }
    }
}
