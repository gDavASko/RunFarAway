using System;

namespace RFW
{
    public interface IInput: IDisposable
    {
        System.Action<ActionType, bool> OnAction { get; set; }
    }
}