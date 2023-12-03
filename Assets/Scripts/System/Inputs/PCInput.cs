using System;
using UnityEngine;

namespace RFW
{
    public class PCInput : IInput, ITickable
    {
        private KeyCode _jumpKeyCode = KeyCode.Space;
        private KeyCode _attackCode = KeyCode.LeftControl;

        public Action<ActionType, bool> OnAction
        {
            get;
            set;
        }

        public void Tick(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(_jumpKeyCode))
            {
                OnAction?.Invoke(ActionType.Jump, true);
            }

            if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(_attackCode))
            {
                OnAction?.Invoke(ActionType.Attack, true);
            }

        }

        public void Dispose()
        {
            OnAction = null;
        }
    }
}