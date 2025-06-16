using UnityEngine;

public class TargetAutoPlacement : MonoBehaviour
{
    public Transform arCamera;      // AR Camera 연결
    public Vector3 offset = new Vector3(0, -0.2f, 1.2f);  // 카메라 기준 거리
    private bool hasPlaced = false;

    void Update()
    {
        if (!hasPlaced && arCamera != null)
        {
            transform.position = arCamera.position + arCamera.forward * offset.z + arCamera.up * offset.y;
            transform.rotation = Quaternion.LookRotation(arCamera.forward); // 카메라 바라보도록
            hasPlaced = true; // 한 번만 배치하고 멈
        }
    }
}

