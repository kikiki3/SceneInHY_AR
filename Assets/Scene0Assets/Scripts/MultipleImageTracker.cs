using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class MultipleImageTracker : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public string nextSceneName = "Scene0_B3_Potal";
    public static string scannedImageName;
    void Start()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            scannedImageName = trackedImage.referenceImage.name;
            SceneManager.LoadScene(nextSceneName);
            break;
        }
    }
}
