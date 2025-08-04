using System.Linq;
using System.Reflection;
using UnityEngine;
using InvalidOperationException = System.InvalidOperationException;

namespace Evalve.Systems
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _currentState;

        public void ChangeState<T>(params object[] args) where T : State
        {
            var type = typeof(T);
            args = args.Prepend(this).ToArray();
        
            var constructor = type.GetConstructors()
                .FirstOrDefault(c => 
                {
                    var parameters = c.GetParameters();
                    if (parameters.Length != args.Length) return false;
                    
                    for (var i = 0; i < parameters.Length; i++)
                    {
                        if (args[i] != null && !parameters[i].ParameterType.IsAssignableFrom(args[i].GetType()))
                            return false;
                    }
                    return true;
                });

            if (constructor == null)
            {
                throw new InvalidOperationException(
                    $"No suitable constructor found for type {type.Name} with provided arguments");
            }

            try
            {
                _currentState?.Exit();
                _currentState = (T)constructor.Invoke(args);
                _currentState?.Enter();
            }
            catch (TargetInvocationException ex)
            {
                throw new InvalidOperationException(
                    $"Failed to create instance of {type.Name}: {ex.InnerException?.Message}", 
                    ex.InnerException);
            }
        }

        private void Update() => _currentState?.Update();
    }
}