using UnityEngine;

public class PrefabPlacementChecker : MonoBehaviour
{
    public GameObject playButtonObject;
    public bool hasPlayedTimeline = false;

    void Start()
    {
        if (playButtonObject != null)
            playButtonObject.SetActive(false);
    }

    void Update()
    {
        if (hasPlayedTimeline)
        {
            if (playButtonObject.activeSelf)
                playButtonObject.SetActive(false);
            return;
        }

        bool manwolPlaced = IsPlacedClone("manwol");
        bool chansungPlaced = IsPlacedClone("chansung");
        bool ghostPlaced = IsPlacedClone("ghost");

        if (manwolPlaced && chansungPlaced && ghostPlaced)
        {
            if (!playButtonObject.activeSelf)
                playButtonObject.SetActive(true);
        }
        else
        {
            if (playButtonObject.activeSelf)
                playButtonObject.SetActive(false);
        }
    }

    bool IsPlacedClone(string targetName)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            string lowerName = obj.name.ToLower();
            if (lowerName == targetName.ToLower() + "(clone)")
            {
                return true;
            }
        }
        return false;
    }

    //외부에서 호출 가능하도록 함수 추가
    public void MarkTimelinePlayed()
    {
        hasPlayedTimeline = true;
    }
}
