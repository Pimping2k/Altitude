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
        [SerializeField] private float _rotationSensitivity = 5f;
        [SerializeField] private Vector2 _rotationClamedValue;
        
        private IInputService _inputService;
        private ICameraService _cameraService;

        private Vector2 _moveInput;
        private Vector3 _movement;
        private Vector2 _lookInput;
        private Vector3 _currentRotation;
        
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
            var cameraForward = _camera.transform.forward;
            var cameraRight = _camera.transform.right;
            
            cameraForward.y = 0f;
            cameraRight.y = 0f;
            
            cameraForward.Normalize();
            cameraRight.Normalize();
            
            _movement = cameraForward * _moveInput.y + cameraRight * _moveInput.x;
            _rigidbody.linearVelocity = _movement * _speed;
            
            _currentRotation.x += _lookInput.y * _rotationSensitivity * Time.fixedDeltaTime * -1;
            _currentRotation.y += _lookInput.x * _rotationSensitivity * Time.fixedDeltaTime;
    
            _currentRotation.x = Mathf.Clamp(_currentRotation.x, _rotationClamedValue.x, _rotationClamedValue.y);
            transform.rotation = Quaternion.Euler(_currentRotation);
        }

        private void ToggleControl(bool state)
        {
            _cameraService.OverlapCamera(_camera);
            
            if(state)
            {
                _inputService.Player.Move.performed += OnMovePerformed;
                _inputService.Player.Move.canceled += OnMoveCanceled;
                
                _inputService.Player.Look.performed += OnLookPerformed;
                _inputService.Player.Look.canceled += OnLookCanceled;
            }
            else
            {
                _inputService.Player.Move.performed -= OnMovePerformed;
                _inputService.Player.Move.canceled -= OnMoveCanceled;
                
                _inputService.Player.Look.performed -= OnLookPerformed;
                _inputService.Player.Look.canceled -= OnLookCanceled;
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

        private void OnLookPerformed(InputAction.CallbackContext ctx)
        {
            _lookInput = ctx.ReadValue<Vector2>();
        }

        private void OnLookCanceled(InputAction.CallbackContext ctx)
        {
            _lookInput = Vector2.zero;
        }
    }
}