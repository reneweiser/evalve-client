using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evalve.App.Ui.Elements
{
    public class Column : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Button _button;
        [SerializeField] private Header _header;
        [SerializeField] private Text _text;
        [SerializeField] private ListButton _listButton;
        [SerializeField] private MultiSelect _multiSelect;
        [SerializeField] private Select _select;
        [SerializeField] private Submit _submit;
        [SerializeField] private TextInput _textInput;
        [SerializeField] private Toggle _toggle;

        [Header("Dependencies")]
        [SerializeField] private Transform _content;

        private readonly List<Element> _elements = new();
        private Dictionary<Type,Element> _prefabs;

        public void Clear()
        {
            foreach (var element in _elements)
                Destroy(element.gameObject);
            _elements.Clear();
        }

        public T Add<T>(string label) where T : Element
        {
            _prefabs ??= new Dictionary<Type, Element>()
            {
                [typeof(Button)] = _button,
                [typeof(Header)] = _header,
                [typeof(Text)] = _text,
                [typeof(ListButton)] = _listButton,
                [typeof(MultiSelect)] = _multiSelect,
                [typeof(Select)] = _select,
                [typeof(Submit)] = _submit,
                [typeof(TextInput)] = _textInput,
                [typeof(Toggle)] = _toggle,
            };

            return Add(label, _prefabs[typeof(T)]) as T;
        }

        private Element Add(string label, Element prefab)
        {
            var element = Instantiate(prefab, _content);
            element.Label = label;
            _elements.Add(element);
            
            return element;
        }
    }
}