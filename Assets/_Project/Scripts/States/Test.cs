using Evalve.Panels;
using Evalve.Systems;
using UnityEngine;

namespace Evalve.States
{
    public class Test : State
    {
        private readonly ElementsMenu _ui;

        public Test(StateMachine stateMachine) : base(stateMachine)
        {
            _ui = Services.Get<SceneObject>().Show<ElementsMenu>();
        }

        public override void Enter()
        {
            var assets = _ui.CreateMenuItem("Assets");
            assets.AddCommand("Test", () => Debug.Log("assets added"));
            
            var objects = _ui.CreateMenuItem("Objects");
            objects.AddCommand("Test", () => Debug.Log("objects added"));
        }

        public override void Exit() { }

        public override void Update() { }
    }
}