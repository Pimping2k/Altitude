using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Services
{
    public class CameraService : MonoBehaviour, ICameraService
    {
        private const int ACTIVE_CAMERA_PRIORITY = 999;
        private const int UNACTIVE_CAMERA_PRIORITY = -999;
        
        [Header("General Settings")]
        [SerializeField] private Camera _mainUnityCamera;
        [SerializeField] private CinemachineBrain _cinemachineBrain;
        
        private CinemachineCamera _currentVirtualCamera;
        private CinemachineCamera _previousVirtualCamera;
        
        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(_mainUnityCamera.gameObject);
            IsInitialized = true;
        }

        #region Utilities

        public void OverlapCamera(CinemachineCamera overlapCamera)
        {
            if (_currentVirtualCamera != null)
            {
                _previousVirtualCamera = _currentVirtualCamera;
                _previousVirtualCamera.Priority = UNACTIVE_CAMERA_PRIORITY;
            }

            overlapCamera.Priority = ACTIVE_CAMERA_PRIORITY;
            _currentVirtualCamera = overlapCamera;
        }

        public void RemoveOverlapCamera()
        {
            if (_previousVirtualCamera == null) return;
            
            _currentVirtualCamera.Priority = UNACTIVE_CAMERA_PRIORITY;
            _previousVirtualCamera.Priority = ACTIVE_CAMERA_PRIORITY;
            _currentVirtualCamera = _previousVirtualCamera;
        }
        
        public CinemachineCamera GetCamera()
        {
            return _currentVirtualCamera;
        }
        
        public Camera GetUnityCamera()
        {
            return _mainUnityCamera;
        }

        #endregion
    }
}