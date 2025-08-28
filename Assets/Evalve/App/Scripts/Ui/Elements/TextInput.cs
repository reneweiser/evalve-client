using System;
using TMPro;
using UnityEngine;

namespace Evalve.App.Ui.Elements
{
    public class TextInput : Element
    {
        public event Action<string> InputChanged;

        [SerializeField] private TMP_InputField _input;

        private void Awake()
        {
            _input.onValueChanged.AddListener(input => InputChanged?.Invoke(input));
        }

        public void SetValue(string text)
        {
            _input.SetTextWithoutNotify(text);
        }

        public void SetType(TextInputType type)
        {
            _input.contentType = type switch
            {
                TextInputType.Text => TMP_InputField.ContentType.Standard,
                TextInputType.Number => TMP_InputField.ContentType.IntegerNumber,
                TextInputType.Password => TMP_InputField.ContentType.Password,
                TextInputType.Email => TMP_InputField.ContentType.EmailAddress,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
