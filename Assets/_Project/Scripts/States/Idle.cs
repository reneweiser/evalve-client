using Evalve.Panels;
using Evalve.Systems;

namespace Evalve.States
{
    public class Idle : State
    {
        private bool _isVisible;

        public Idle()
        {
        }

        public override void Enter()
        {
            _inputUse.canceled += SelectObject;
            _inputMenu.started += OpenMenu;
            
            Services.Get<Ui>().Show<Panels.Idle>();
        }

        public override void Exit()
        {
            _inputUse.canceled -= SelectObject;
            _inputMenu.started -= OpenMenu;
        }

        public override void Update() { }
    }
}