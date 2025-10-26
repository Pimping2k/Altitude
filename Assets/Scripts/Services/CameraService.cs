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
        [SerializeField] private CinemachineCamera _mainCamera;
        
        private CinemachineCamera _previousCamera;
        
        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            InitializeCamera();
            IsInitialized = true;
        }

        private void InitializeCamera()
        {
            var cameraInstance = Instantiate(_mainCamera);
            DontDestroyOnLoad(cameraInstance);
            _mainCamera.Priority = ACTIVE_CAMERA_PRIORITY;
        }

        #region Utilities

        public void OverlapCamera(CinemachineCamera overlapCamera)
        {
            _previousCamera = _mainCamera;
            
            _previousCamera.Priority = UNACTIVE_CAMERA_PRIORITY;
            overlapCamera.Priority = ACTIVE_CAMERA_PRIORITY;
            
            _mainCamera = overlapCamera;
        }

        public void RemoveOverlapCamera()
        {
            _mainCamera.Priority = UNACTIVE_CAMERA_PRIORITY;
            _previousCamera.Priority = ACTIVE_CAMERA_PRIORITY;
            
            _mainCamera = _previousCamera;
        }
        
        public CinemachineCamera GetCamera()
        {
            return _mainCamera;
        }

        #endregion
    }
}