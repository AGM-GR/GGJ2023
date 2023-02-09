using Cinemachine;
using System.Linq;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    public CarColor carColor;
    private CinemachineVirtualCamera vCam;

    public void Setup(Character character)
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        vCam.LookAt = character.refCameraPoint;
        vCam.Follow = character.transform;
    }

    public void ZoomIn()
    {
        vCam.Priority = 12;
    }

    public void ZoomOut()
    {
        vCam.Priority = 0;
    }
}
