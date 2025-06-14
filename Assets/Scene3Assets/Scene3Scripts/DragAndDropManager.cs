using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class DragAndDropManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefabToSpawn;         // ����� ������
    public GameObject dragPreviewPrefab;     // �巡�� �� ����ٴ� ������ ������Ʈ
    public string prefabTag;                 // ex: "ghost", "chansung", "manwol"
    public Transform targetPosition;         // ���� Ÿ�� ������
    public float maxOffset = 0.3f;           // ��ġ ��� ����

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
                Debug.LogWarning("Ÿ�� Transform�� ��� �ֽ��ϴ�.");
                Destroy(previewInstance);
                return;
            }

            // ��ġ ��� ���� Ȯ��
            if (Vector3.Distance(hitPose.position, targetPosition.position) > maxOffset)
            {
                Debug.LogWarning("Ÿ�� ��ġ���� �ʹ� �ٴϴ�. ��ġ ��ҵ�.");
                Destroy(previewInstance);
                return;
            }

            // ���� ������Ʈ ����
            GameObject existing = GameObject.FindGameObjectWithTag(prefabTag);
            if (existing != null)
                Destroy(existing);

            // �� ������Ʈ ��ġ (Ÿ�� ��ġ �� ȸ������ ��Ȯ�� ��ġ)
            GameObject obj = Instantiate(prefabToSpawn, targetPosition.position, targetPosition.rotation);
            obj.tag = prefabTag;

            // Ÿ�� ������Ʈ ����
           targetPosition.gameObject.SetActive(false);

            // Ÿ�Ӷ��� ���ε�
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
