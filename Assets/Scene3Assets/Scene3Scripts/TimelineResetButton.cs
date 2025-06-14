using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TimelineResetButton : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject playButton;
    public GameObject resetButton;
    public PrefabPlacementChecker placementChecker;

    // Ÿ�� ������ ������Ʈ��
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;

    private bool isTimelineManuallyStarted = false;
    private bool hasTimelineEnded = false;
    private float timelineDuration;
    private float timelineTimer = 0f;

    void Start()
    {
        if (playButton != null)
            playButton.SetActive(false);

        if (resetButton != null)
            resetButton.SetActive(false);

        if (director != null)
        {
            timelineDuration = (float)director.duration;
            timelineTimer = 0f;
            Debug.Log($"[Start] Ÿ�Ӷ��� ����: {timelineDuration:F2}��");
        }
    }

    void Update()
    {
        if (isTimelineManuallyStarted && !hasTimelineEnded)
        {
            timelineTimer += Time.deltaTime;

            if (timelineTimer >= timelineDuration)
            {
                Debug.Log("[Update] Ÿ�̸� �������� Ÿ�Ӷ��� ���� ����");
                OnTimelineEnd();
            }
        }
    }

    public void StartTimeline()
    {
        if (director != null && !hasTimelineEnded)
        {
            director.Play();
            isTimelineManuallyStarted = true;
            timelineTimer = 0f;

            if (playButton != null)
                playButton.SetActive(false);
        }
    }

    void OnTimelineEnd()
    {
        hasTimelineEnded = true;

        if (placementChecker != null)
            placementChecker.hasPlayedTimeline = true;

        if (resetButton != null)
            resetButton.SetActive(true);

        Debug.Log("Ÿ�Ӷ��� ���� �� ���� ��ư Ȱ��ȭ");
    }

    public void ResetTimeline()
    {
        Debug.Log("���� ��ư Ŭ����");

        string[] tags = { "chansung", "ghost", "manwol" };
        foreach (string tag in tags)
        {
            GameObject obj = GameObject.FindWithTag(tag);
            if (obj != null)
                Destroy(obj);
        }

        isTimelineManuallyStarted = false;
        hasTimelineEnded = false;
        timelineTimer = 0f;

        if (placementChecker != null)
            placementChecker.hasPlayedTimeline = false;

        if (resetButton != null)
            resetButton.SetActive(false);

        // Ÿ�Ӷ��� �ð� �ʱ�ȭ
        if (director != null)
        {
            director.time = 0;
            director.Evaluate(); // �ٷ� ���� ����
            director.Pause();
        }

        // Ÿ�� ������ �ٽ� ���̰�
        if (target1 != null) target1.SetActive(true);
        if (target2 != null) target2.SetActive(true);
        if (target3 != null) target3.SetActive(true);
    }
}
