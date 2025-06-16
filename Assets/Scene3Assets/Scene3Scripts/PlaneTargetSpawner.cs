using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class PlaneTargetSpawner : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public Transform[] targetObjects; // 하이어라키에 있는 캡슐들

    private bool isPlaced = false;

    void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (isPlaced) return;

        if (args.added != null && args.added.Count > 0)
        {
            ARPlane detectedPlane = args.added[0];

            // 60cm x 20cm 이상일 때만 배치
            if (detectedPlane.size.x >= 0.6f && detectedPlane.size.y >= 0.2f)
            {
                float planeY = detectedPlane.center.y;

                for (int i = 0; i < targetObjects.Length; i++)
                {
                    Vector3 oldPos = targetObjects[i].position;
                    targetObjects[i].position = new Vector3(oldPos.x, planeY, oldPos.z);

                    Debug.Log($"[Target {i}] Plane Y: {planeY} → 위치 적용됨: {targetObjects[i].position}");
                }

                Debug.Log(" Plane 충분히 큼! 배치 완료.");
                isPlaced = true;
            }
            else
            {
                Debug.LogWarning($"Plane 너무 작음! 현재 크기: {detectedPlane.size.x:F2} x {detectedPlane.size.y:F2}");
            }
        }
    }

}
