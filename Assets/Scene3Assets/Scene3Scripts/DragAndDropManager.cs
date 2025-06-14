using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class DragAndDropManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefabToSpawn;         // 드롭할 프리팹
    public GameObject dragPreviewPrefab;     // 드래그 중 따라다닐 프리뷰 오브젝트
    public string prefabTag;                 // ex: "ghost", "chansung", "manwol"
    public Transform targetPosition;         // 단일 타겟 포지션
    public float maxOffset = 0.3f;           // 위치 허용 오차

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

        if (raycastManager.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (targetPosition == null)
            {
                Debug.LogWarning("타겟 Transform이 비어 있습니다.");
                Destroy(previewInstance);
                return;
            }

            // 위치 허용 오차 확인
            if (Vector3.Distance(hitPose.position, targetPosition.position) > maxOffset)
            {
                Debug.LogWarning("타겟 위치에서 너무 멉니다. 배치 취소됨.");
                Destroy(previewInstance);
                return;
            }

            // 기존 오브젝트 제거
            GameObject existing = GameObject.FindGameObjectWithTag(prefabTag);
            if (existing != null)
                Destroy(existing);

            // 새 오브젝트 배치 (타겟 위치 및 회전으로 정확히 배치)
            GameObject obj = Instantiate(prefabToSpawn, targetPosition.position, targetPosition.rotation);
            obj.tag = prefabTag;

            // 타겟 오브젝트 제거
           targetPosition.gameObject.SetActive(false);

            // 타임라인 바인딩
            TimelineBinder binder = FindObjectOfType<TimelineBinder>();
            if (binder != null)
            {
                if (prefabTag == "chansung") binder.chansung = obj;
                else if (prefabTag == "ghost") binder.ghost = obj;
                else if (prefabTag == "manwol") binder.manwol = obj;

                binder.BindAllCharacters();
            }
        }

        if (previewInstance != null)
            Destroy(previewInstance);
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
