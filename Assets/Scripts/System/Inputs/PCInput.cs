using System;
using UnityEngine;

namespace RFW
{
    public class PCInput : IInput, ITickable
    {
        private KeyCode _jumpKeyCode = KeyCode.Space;

        public Action<ActionType> OnAction { get; set; }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(_jumpKeyCode))
            {
                OnAction?.Invoke(ActionType.Jump);
            }

            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(_jumpKeyCode))
            {
                OnAction?.Invoke(ActionType.Attack);
            }
        }
    }
}