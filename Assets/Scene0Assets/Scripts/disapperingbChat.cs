using UnityEngine;
using System.Collections;

public class disapperingbChat : MonoBehaviour
{
    public GameObject messagePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        messagePanel.SetActive(true);
        StartCoroutine(HideMessageAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(8.5f);
        messagePanel.SetActive(false);
    }
}
