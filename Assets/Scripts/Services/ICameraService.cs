using Service_Locator;
using Unity.Cinemachine;
using UnityEngine;

namespace Services
{
    public interface ICameraService : IService
    {
        public CinemachineCamera GetCamera();
        public Camera GetUnityCamera();
        public void OverlapCamera(CinemachineCamera overlapCamera);
        public void RemoveOverlapCamera();
    }
}