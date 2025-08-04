using System;
using Evalve.States;
using UnityEngine;
using UnityEngine.UI;

namespace Evalve.Panels
{
    public class SceneObjectTools : UiPanel
    {
        public event Action<ToolType> ToolSelected;
        public event Action Canceled;
        
        [SerializeField] private Button _selectCreate;
        [SerializeField] private Button _selectSelect;
        [SerializeField] private Button _cancel;

        private void Awake()
        {
            _selectCreate.onClick.AddListener(() => ToolSelected?.Invoke(ToolType.Create));
            _selectSelect.onClick.AddListener(() => ToolSelected?.Invoke(ToolType.Select));
            _cancel.onClick.AddListener(() => Canceled?.Invoke());
        }

        private void OnDisable()
        {
            ToolSelected = null;
            Canceled = null;
        }
    }
}