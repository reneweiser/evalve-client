using System;

namespace Evalve.Contracts
{
    public interface ILogger
    {
        event Action<string> Logged;
        void Log(string message, LogType type);
    }
}