using System;

namespace Evalve
{
    public class Logger
    {
        public static event Action<string> EntryLogged;

        public static void Log(string message)
        {
            EntryLogged?.Invoke(DateTime.Now + ": " + message);
        }
    }
}