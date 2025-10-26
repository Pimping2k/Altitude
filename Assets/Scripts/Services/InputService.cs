using System;
using UnityEngine;

namespace Services
{
    public class InputService : MonoBehaviour, IInputService
    {
        private InputSystem_Actions _inputSystem;

        private readonly CursorLockMode _cursorLockMode = CursorLockMode.Locked;
        
        public CursorLockMode CursorLockMode => _cursorLockMode;
        public InputSystem_Actions InputSystem { get; private set; }
        public InputSystem_Actions.UIActions UI { get; private set; }
        public InputSystem_Actions.PlayerActions Player { get; private set; }

        public bool IsInitialized { get; private set; }
        
        private void Awake()
        {
            _inputSystem = new InputSystem_Actions();

            Cursor.lockState = _cursorLockMode;
            Cursor.visible = _cursorLockMode == CursorLockMode.Locked;

            Player = _inputSystem.Player;
            UI = _inputSystem.UI;

            _inputSystem.Enable();
            Player.Enable();
            
            IsInitialized = true;
        }
    }
}