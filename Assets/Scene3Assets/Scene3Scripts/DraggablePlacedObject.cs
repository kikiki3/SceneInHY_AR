using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class DraggablePlacedObject : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private Camera arCamera;
    private bool isDragging = false;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arCamera = Camera.main;
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        Vector2 touchPos = touch.position;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                Ray ray = arCamera.ScreenPointToRay(touchPos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    isDragging = true;
                }
                break;

            case TouchPhase.Moved:
                if (isDragging && raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
                {
                    transform.position = hits[0].pose.position;
                }
                break;

            case TouchPhase.Ended:
                isDragging = false;
                break;
        }
    }
}

