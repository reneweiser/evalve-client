using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evalve.Panels.Elements
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
        public string Label => _text.text;

        public void SetLabel(string label)
        {
            _text.text = label;
        }

        public void SetCommand(Action action)
        {
            _button.onClick.AddListener(action.Invoke);
        }

        public void SetIsSelected(bool isSelected)
        {
            _image.color = isSelected ? _activeColor : _inactiveColor;
        }
    }
}