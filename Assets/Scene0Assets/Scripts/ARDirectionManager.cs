using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ARDirectionManager : MonoBehaviour
{
    public Transform arCamera;
    public GameObject frontPrefab;
    public GameObject leftPrefab;
    public GameObject rightPrefab;

    private GameObject frontInstance;
    private GameObject leftInstance;
    private GameObject rightInstance;

    public float distanceFromCamera = 1.5f;

    void Start()
    {
        Vector3 camPos = arCamera.position;
        Vector3 forward = new Vector3(arCamera.forward.x, 0, arCamera.forward.z).normalized;
        Vector3 right = new Vector3(arCamera.right.x, 0, arCamera.right.z).normalized;

        frontInstance = Instantiate(frontPrefab, camPos + forward * distanceFromCamera, Quaternion.identity);
        leftInstance = Instantiate(leftPrefab, camPos - right * distanceFromCamera, Quaternion.identity);
        rightInstance = Instantiate(rightPrefab, camPos + right * distanceFromCamera, Quaternion.identity);

        frontInstance.SetActive(false);
        leftInstance.SetActive(false);
        rightInstance.SetActive(false);
    }

    void Update()
    {
        float y = arCamera.eulerAngles.y;

        if (y >= 330f || y <= 30f)
        {
            ShowOnly(frontInstance);
        }
        else if (y >= 60f && y <= 150f)
        {
            ShowOnly(leftInstance);
        }
        else if (y >= 210f && y <= 300f)
        {
            ShowOnly(rightInstance);
        }
        else
        {
            ShowOnly(null);
        }

        // Always face the camera
        FaceCamera(frontInstance);
        FaceCamera(leftInstance);
        FaceCamera(rightInstance);

        // Touch interaction
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            Ray ray = arCamera.GetComponent<Camera>().ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Selected: " + hit.transform.name);
                if (hit.transform == frontInstance.transform)
                {
                    SceneManager.LoadScene("Scene2");
                }
                else if (hit.transform == leftInstance.transform)
                {
                    SceneManager.LoadScene("SG0");
                }
                else if (hit.transform == rightInstance.transform)
                {
                    SceneManager.LoadScene("Scene3");
                }
            }
        }
    }

    void ShowOnly(GameObject target)
    {
        frontInstance.SetActive(target == frontInstance);
        leftInstance.SetActive(target == leftInstance);
        rightInstance.SetActive(target == rightInstance);
    }

    void FaceCamera(GameObject obj)
    {
        if (obj != null && obj.activeSelf)
        {
            Vector3 direction = obj.transform.position - arCamera.position;
            direction.y = 0; // Only rotate on Y axis
            obj.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
