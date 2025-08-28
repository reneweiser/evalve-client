using System;
using Evalve.Contracts;

namespace Evalve.App
{
    public class EvalveLogger : ILogger
    {
        public event Action<string> Logged;
        
        public void Log(string message, LogType type)
        {
            switch (type)
            {
                case LogType.Message:
                    Logged?.Invoke($"{DateTime.Now}: {message}");
                    break;
                case LogType.Warning:
                    Logged?.Invoke($"{DateTime.Now}: <color=yellow>{message}</color>");
                    break;
                case LogType.Error:
                    Logged?.Invoke($"{DateTime.Now}: <color=red>{message}</color>");
                    break;
                case LogType.Success:
                    Logged?.Invoke($"{DateTime.Now}: <color=green>{message}</color>");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}