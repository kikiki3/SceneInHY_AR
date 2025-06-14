using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class DragAndDropManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefabToSpawn;         // 드롭할 프리팹
    public GameObject dragPreviewPrefab;     // 드래그 중 따라다닐 프리뷰 오브젝트
    public string prefabTag;

    private GameObject previewInstance;
    private Camera arCamera;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arCamera = Camera.main;
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 프리뷰 생성
        previewInstance = Instantiate(dragPreviewPrefab);
        SetTransparency(previewInstance, 0.5f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 screenPos = eventData.position;

        if (raycastManager.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            previewInstance.transform.position = hitPose.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 screenPos = eventData.position;

        // Plane 위인지 확인
        if (raycastManager.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            // 기존 오브젝트 있으면 제거
            GameObject existing = GameObject.FindGameObjectWithTag(prefabTag);
            if (existing != null)
            {
                Destroy(existing);
                Debug.Log($"기존 '{prefabTag}' 오브젝트 삭제 후 새로 배치");
            }

            // 새 프리팹 배치
            GameObject obj = Instantiate(prefabToSpawn, hitPose.position, hitPose.rotation);
            obj.tag = prefabTag;
        }

        // 프리뷰 제거
        if (previewInstance != null)
        {
            Destroy(previewInstance);
        }
    }


    void SetTransparency(GameObject obj, float alpha)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            if (r.material.HasProperty("_Color"))
            {
                Color c = r.material.color;
                c.a = alpha;
                r.material.color = c;
            }
        }
    }
}

