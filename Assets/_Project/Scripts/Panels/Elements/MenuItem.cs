using System;

namespace Evalve.Panels.Elements
{
    public class MenuItem
    {
        private readonly MenuButton _menuButton;
        private readonly MenuPanel _menuPanel;

        public MenuItem(string label, MenuButton menuButton, MenuPanel menuPanel, ElementsMenu elementsMenu)
        {
            _menuButton = menuButton;
            _menuPanel = menuPanel;
            
            _menuButton.SetLabel(label);
            _menuButton.SetCommand(() => elementsMenu.Show(this));
        }

        public void SetActive(bool active)
        {
            _menuButton.SetIsSelected(active);
            _menuPanel.SetIsSelected(active);
        }

        public void AddCommand(string label, Action action)
        {
            _menuPanel.AddCommand(label, action.Invoke);
        }

        public void AddToggleCommand(string label, Action action)
        {
            _menuPanel.AddToggleCommand(label, action.Invoke);
        }

        public void RemoveCommand(string label)
        {
            _menuPanel.RemoveCommand(label);
        }
    }
}