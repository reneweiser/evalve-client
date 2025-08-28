using System;
using UnityEngine;

namespace Evalve.App.Ui.Elements
{
    public class Submit : Element
    {
        public event Action Clicked;
        
        [SerializeField] private UnityEngine.UI.Button _submitButton;

        private void Awake()
        {
            _submitButton.onClick.AddListener(() => Clicked?.Invoke());
        }

        public void SetIsInteractable(bool isSubmitActive)
        {
            _submitButton.interactable = isSubmitActive;
            _label.alpha = isSubmitActive ? 1f : .5f;
        }
    }
}