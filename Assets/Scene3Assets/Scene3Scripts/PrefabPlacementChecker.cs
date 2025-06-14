using UnityEngine;

public class PrefabPlacementChecker : MonoBehaviour
{
    public GameObject playButtonObject;  // �ν����Ϳ� ����� ����

    void Start()
    {
        if (playButtonObject != null)
            playButtonObject.SetActive(false);  // ���� �� ��Ȱ��ȭ
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
