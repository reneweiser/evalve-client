using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Evalve.App.Ui.Elements
{
    public class Select : Element
    {
        public event Action<string> InputChanged;
        
        [SerializeField] private TMP_Dropdown _dropdown;
        
        private Dictionary<string,string> _options = new();

        private void Awake()
        {
            _dropdown.onValueChanged.AddListener(OnInputChanged);
        }

        public void SetOptions(Dictionary<string,string> options)
        {
            _options = options;
            _dropdown.ClearOptions();
            _dropdown.AddOptions(options.Select(o => o.Value).ToList());
        }

        private void OnInputChanged(int index)
        {
            InputChanged?.Invoke(_options.Keys.ElementAt(index));
        }

        public void SetValue(string poseRole)
        {
            _dropdown.SetValueWithoutNotify(_options.Keys.ToList().IndexOf(poseRole));
        }
    }
}