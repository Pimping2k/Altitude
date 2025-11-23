using System;
using Service_Locator;
using Services;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerRelated
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private Rigidbody _rigidbody;
        [Header("General Settings")]
        [SerializeField] private float _speed = 5f;
        
        private IInputService _inputService;
        private ICameraService _cameraService;

        private Vector2 _moveInput;
        private Vector3 _movement;
        
        private void Awake()
        {
            _inputService = ServiceLocator.Resolve<IInputService>();
            _cameraService = ServiceLocator.Resolve<ICameraService>();
        }

        private void Start()
        {
            ToggleControl(true);
        }

        private void OnDestroy()
        {
            ToggleControl(false);
        }

        private void FixedUpdate()
        {
            _movement = new Vector3(_moveInput.x, 0f, _moveInput.y);
            _rigidbody.linearVelocity = _movement * _speed * Time.fixedDeltaTime;
        }

        private void ToggleControl(bool state)
        {
            _cameraService.OverlapCamera(_camera);
            
            if(state)
            {
                _inputService.Player.Move.performed += OnMovePerformed;
                _inputService.Player.Move.canceled += OnMoveCanceled;
                _inputService.Player.Jump.performed += OnJumpPerformed;
            }
            else
            {
                _inputService.Player.Move.performed -= OnMovePerformed;
                _inputService.Player.Move.canceled -= OnMoveCanceled;
            }
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            _moveInput = ctx.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            _moveInput = Vector2.zero;
        }

        private void OnJumpPerformed(InputAction.CallbackContext ctx)
        {
            _cameraService.RemoveOverlapCamera();
        }
    }
}