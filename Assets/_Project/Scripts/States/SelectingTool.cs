using System;
using Evalve.Panels;
using Evalve.Systems;

namespace Evalve.States
{
    public class SelectingTool : State
    {
        private readonly SceneObjectTools _ui;

        public SelectingTool(StateMachine stateMachine) : base(stateMachine)
        {
            _ui = Services.Get<Ui>().Show<SceneObjectTools>();
        }

        public override void Enter()
        {
            _ui.ToolSelected += OnToolSelected;
            _ui.Canceled += Back;
        }

        public override void Update() { }

        public override void Exit()
        {
            _ui.ToolSelected -= OnToolSelected;
            _ui.Canceled -= Back;
        }

        private void OnToolSelected(ToolType toolType)
        {
            switch (toolType)
            {
                case ToolType.Create:
                    _stateMachine.ChangeState<PlacingObject>();
                    break;
                case ToolType.Select:
                    _stateMachine.ChangeState<SelectingObject>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toolType), toolType, null);
            }
        }

        private void Back()
        {
            _stateMachine.ChangeState<Idle>();
        }
    }
}