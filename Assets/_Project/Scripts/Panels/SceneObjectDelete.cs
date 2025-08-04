using System;
using UnityEngine;
using UnityEngine.UI;

namespace Evalve.Panels
{
    public class SceneObjectDelete : UiPanel
    {
        public event Action ObjectDeleted;
        public event Action Canceled;
        
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _cancelButton;

        private void OnEnable()
        {
            _deleteButton.onClick.AddListener(() => ObjectDeleted?.Invoke());
            _cancelButton.onClick.AddListener(() => Canceled?.Invoke());
        }

        private void OnDisable()
        {
            _deleteButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.RemoveAllListeners();
            ObjectDeleted = null;
            Canceled = null;
        }
    }
}