using System;
using UnityEngine;

namespace Services
{
    public class InputService : MonoBehaviour, IInputService
    {
        private InputSystem_Actions _inputSystem;
        
        public CursorLockMode CursorLockMode { get; } = CursorLockMode.Locked;

        public InputSystem_Actions InputSystem { get; private set; }
        public InputSystem_Actions.UIActions UI { get; private set; }
        public InputSystem_Actions.PlayerActions Player { get; private set; }

        public bool IsInitialized { get; private set; }
        
        private void Awake()
        {
            _inputSystem = new InputSystem_Actions();

            Cursor.lockState = CursorLockMode;
            Cursor.visible = CursorLockMode == CursorLockMode.Locked;

            Player = _inputSystem.Player;
            UI = _inputSystem.UI;

            _inputSystem.Enable();
            Player.Enable();
            
            IsInitialized = true;
        }
    }
}