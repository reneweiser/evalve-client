using Evalve.Panels;
using Evalve.Systems;

namespace Evalve.States
{
    public class UsingElementsMenu : State
    {
        public UsingElementsMenu()
        {
            Services.Get<Ui>().Show<ElementsMenu>();
        }

        public override void Enter()
        {
            _inputUse.canceled += SelectObject;
        }

        public override void Exit()
        {
            _inputUse.canceled -= SelectObject;
        }

        public override void Update() { }
    }
}