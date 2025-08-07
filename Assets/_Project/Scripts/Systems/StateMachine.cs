using UnityEngine;

namespace Evalve.Systems
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _currentState;

        public void ChangeState(State newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        private void Update() => _currentState?.Update();
    }
}