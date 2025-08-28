using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Evalve.App.Ui.Elements
{
    public class MultiSelect : Element
    {
        public event Action<List<string>> InputChanged;
        public bool AllowMultipleSelections { get; set; }
        
        [SerializeField] private Toggle _listButtonPrefab;
        [SerializeField] private Transform _container;

        private readonly Dictionary<string, bool> _result = new();
        private Dictionary<string, string> _options = new();
        
        public void SetOptions(Dictionary<string,string> options)
        {
            _options = options;
            foreach (var option in _options)
            {
                var listButton = Instantiate(_listButtonPrefab, _container);
                listButton.Label = option.Value;
                listButton.InputChanged += input => OnInputChanged(listButton, input, option.Key);
            }
        }

        private void OnInputChanged(Toggle listButton, bool input, string key)
        {
            if (!AllowMultipleSelections)
            {
                _result.Clear();
                foreach (var toggle in _container.GetComponentsInChildren<Toggle>())
                {
                    toggle.SetValue(false);
                }
            }
            
            listButton.SetValue(input);
            _result[key] = input;
            
            var value = _result.Where(item => item.Value)
                .Select(item => item.Key)
                .ToList();
            
            InputChanged?.Invoke(value);
        }
    }
}