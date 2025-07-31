using System;

namespace Evalve
{
    public interface IObjectCommand
    {
        event Action Executed;
        void Initialize();
        void Tick();
    }
}