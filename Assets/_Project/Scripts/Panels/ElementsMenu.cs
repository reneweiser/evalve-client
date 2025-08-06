using System.Collections.Generic;
using Evalve.Panels.Elements;
using UnityEngine;

namespace Evalve.Panels
{
    public class ElementsMenu : UiPanel
    {
        [SerializeField] private MenuButton _buttonPrefab;
        [SerializeField] private MenuPanel _panelPrefab;
        
        [SerializeField] private Transform _buttonContainer;
        [SerializeField] private Transform _panelContainer;

        private readonly List<MenuItem> _items = new();

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