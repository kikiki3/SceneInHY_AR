using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARCameraManager))]
public class SetFrontCamera : MonoBehaviour
{
    void Start()
    {
        var cameraManager = GetComponent<ARCameraManager>();
        if (cameraManager != null)
        {
            cameraManager.requestedFacingDirection = CameraFacingDirection.User;
        }
    }
}
