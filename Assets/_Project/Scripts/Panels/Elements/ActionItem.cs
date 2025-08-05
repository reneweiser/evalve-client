using System;
using Object = UnityEngine.Object;

namespace Evalve.Panels.Elements
{
    public class ActionItem
    {
        private readonly Action _action;
        private readonly MenuButton _button;
        private bool _isSelected;

        public ActionItem(string label, Action action, MenuButton button)
        {
            _action = () =>
            {
                Toggle();
                action.Invoke();
            };
            _button = button;
            _button.SetIsSelected(false);
            _button.SetCommand(_action);
            _button.SetLabel(label);
        }


        private void Toggle()
        {
            _isSelected = !_isSelected;
            _button.SetIsSelected(_isSelected);
        }

        public void Clear()
        {
            Object.Destroy(_button.gameObject);
        }
    }
}