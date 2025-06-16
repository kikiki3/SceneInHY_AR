using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class PlaneTargetSpawner : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public Transform[] targetObjects;               // 타겟 오브젝트 (비활성화 상태)
    public GameObject uiScanPrompt;                 // "바닥을 더 인식해주세요" UI
    public GameObject uiPlacementPrompt;            // "캐릭터를 배치하세요" UI

    private bool isPlaced = false;

    void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    void Start()
    {
        // 처음엔 타겟과 배치 UI 꺼두고, 스캔 UI만 켜기
        foreach (var obj in targetObjects)
            obj.gameObject.SetActive(false);

        if (uiScanPrompt != null)
            uiScanPrompt.SetActive(true);

        if (uiPlacementPrompt != null)
            uiPlacementPrompt.SetActive(false);
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (isPlaced) return;

        if (args.added != null && args.added.Count > 0)
        {
            ARPlane detectedPlane = args.added[0];

            if (detectedPlane.size.x >= 0.4f && detectedPlane.size.y >= 0.2f)
            {
                float planeY = detectedPlane.center.y;

                for (int i = 0; i < targetObjects.Length; i++)
                {
                    Vector3 oldPos = targetObjects[i].position;
                    targetObjects[i].position = new Vector3(oldPos.x, planeY, oldPos.z);
                    targetObjects[i].gameObject.SetActive(true);
                }

                // UI 전환
                if (uiScanPrompt != null)
                    uiScanPrompt.SetActive(false);

                if (uiPlacementPrompt != null)
                    uiPlacementPrompt.SetActive(true);

                Debug.Log(" 충분한 Plane 확보됨 → 타겟 & 배치 UI 활성화");
                isPlaced = true;
            }
            else
            {
                Debug.Log($" Plane 너무 작음: {detectedPlane.size.x:F2} x {detectedPlane.size.y:F2}");
            }
        }
    }
}
