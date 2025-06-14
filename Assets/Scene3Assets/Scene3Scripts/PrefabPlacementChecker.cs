using UnityEngine;

public class PrefabPlacementChecker : MonoBehaviour
{
    public GameObject playButtonObject;  // 인스펙터에 끌어다 놓기

    void Start()
    {
        if (playButtonObject != null)
            playButtonObject.SetActive(false);  // 시작 시 비활성화
    }

    void Update()
    {
        bool manwolPlaced = GameObject.FindGameObjectWithTag("manwol") != null;
        bool chansungPlaced = GameObject.FindGameObjectWithTag("chansung") != null;
        bool ghostPlaced = GameObject.FindGameObjectWithTag("ghost") != null;

        if (manwolPlaced && chansungPlaced && ghostPlaced)
        {
            if (playButtonObject != null && !playButtonObject.activeSelf)
                playButtonObject.SetActive(true);
        }
        else
        {
            if (playButtonObject != null && playButtonObject.activeSelf)
                playButtonObject.SetActive(false);
        }
    }
}
