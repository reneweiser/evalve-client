using System;
using System.Collections.Generic;
using Evalve.Panels.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Evalve.Panels
{
    public class ElementsMenu : UiPanel
    {
        public event Action Hide;
        
        [SerializeField] private MenuButton _buttonPrefab;
        [SerializeField] private MenuPanel _panelPrefab;
        
        [SerializeField] private Transform _buttonContainer;
        [SerializeField] private Transform _panelContainer;
        
        [SerializeField] private Button _hide;

        private readonly List<MenuItem> _items = new();

        private void Awake()
        {
            _hide.onClick.AddListener(() => Hide?.Invoke());
        }

        public MenuItem CreateMenuItem(string label)
        {
            var button = Instantiate(_buttonPrefab, _buttonContainer);
            var panel = Instantiate(_panelPrefab, _panelContainer);
            var item =  new MenuItem(label, button, panel, this);
            _items.Add(item);
            return item;
        }

        public void Show(MenuItem menuItem)
        {
            foreach (var item in _items)
            {
                item.SetActive(item == menuItem);
            }
        }
    }
}