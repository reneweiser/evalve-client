using System;

namespace Evalve
{
    public class Logger
    {
        public static event Action<string> EntryLoggedFormatted;

        public static void Log(string message)
        {
            EntryLoggedFormatted?.Invoke(DateTime.Now + ": " + message);
        }

        public static void LogSuccess(string message)
        {
            EntryLoggedFormatted?.Invoke(DateTime.Now + ": <color=green>" + message + "</color>");
        }

        public static void LogError(string message)
        {
            EntryLoggedFormatted?.Invoke(DateTime.Now + ": <color=red>" + message + "</color>");
        }

        public static void LogWarning(string message)
        {
            EntryLoggedFormatted?.Invoke(DateTime.Now + ": <color=yellow>" + message + "</color>");
        }
    }
}