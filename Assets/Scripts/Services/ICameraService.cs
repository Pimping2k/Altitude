using Service_Locator;
using Unity.Cinemachine;

namespace Services
{
    public interface ICameraService : IService
    {
        public CinemachineCamera GetCamera();
        public void OverlapCamera(CinemachineCamera overlapCamera);
        public void RemoveOverlapCamera();
    }
}