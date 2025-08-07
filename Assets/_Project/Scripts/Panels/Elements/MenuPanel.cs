using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evalve.Panels.Elements
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private MenuButton _prefab;
        
        [SerializeField] private Transform _container;

        private readonly Dictionary<MenuButton, bool> _buttons = new();
        private readonly List<MenuButton> _menuButtons = new();
        
        
        public void SetIsSelected(bool active)
        {
            gameObject.SetActive(active);
        }

        public void AddCommand(string label, Action action)
        {
            var button = Instantiate(_prefab, _container);
            button.SetLabel(label);
            button.SetCommand(action);
            _menuButtons.Add(button);
        }

        public void AddToggleCommand(string label, Action action)
        {
            var button = Instantiate(_prefab, _container);
            button.SetLabel(label);
            button.SetCommand(() =>
            {
                _buttons[button] = !_buttons[button];
                button.SetIsSelected(_buttons[button]);
                action?.Invoke();
            });
            _buttons.Add(button, false);
            _menuButtons.Add(button);
        }

        public void RemoveCommand(string label)
        {
            var button = _menuButtons.Find(btn => btn.Label == label);
            _menuButtons.Remove(button);
            
            if (_buttons.ContainsKey(button))
                _buttons.Remove(button);
            
            Destroy(button.gameObject);
        }
    }
}