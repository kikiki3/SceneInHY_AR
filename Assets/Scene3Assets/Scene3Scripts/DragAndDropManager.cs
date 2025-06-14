using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class DragAndDropManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefabToSpawn;         // ����� ������
    public GameObject dragPreviewPrefab;     // �巡�� �� ����ٴ� ������ ������Ʈ
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
        // ������ ����
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

        // Plane ������ Ȯ��
        if (raycastManager.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            // ���� ������Ʈ ������ ����
            GameObject existing = GameObject.FindGameObjectWithTag(prefabTag);
            if (existing != null)
            {
                Destroy(existing);
                Debug.Log($"���� '{prefabTag}' ������Ʈ ���� �� ���� ��ġ");
            }

            // �� ������ ��ġ
            GameObject obj = Instantiate(prefabToSpawn, hitPose.position, hitPose.rotation);
            obj.tag = prefabTag;
        }

        // ������ ����
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

