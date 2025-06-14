using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TimelineResetButton : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject playButton;
    public GameObject resetButton;
    public PrefabPlacementChecker placementChecker;

    // 타겟 포지션 오브젝트들
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
            Debug.Log($"[Start] 타임라인 길이: {timelineDuration:F2}초");
        }
    }

    void Update()
    {
        if (isTimelineManuallyStarted && !hasTimelineEnded)
        {
            timelineTimer += Time.deltaTime;

            if (timelineTimer >= timelineDuration)
            {
                Debug.Log("[Update] 타이머 기준으로 타임라인 종료 감지");
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

        Debug.Log("타임라인 종료 → 리셋 버튼 활성화");
    }

    public void ResetTimeline()
    {
        Debug.Log("리셋 버튼 클릭됨");

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

        // 타임라인 시간 초기화
        if (director != null)
        {
            director.time = 0;
            director.Evaluate(); // 바로 상태 적용
            director.Pause();
        }

        // 타겟 포지션 다시 보이게
        if (target1 != null) target1.SetActive(true);
        if (target2 != null) target2.SetActive(true);
        if (target3 != null) target3.SetActive(true);
    }
}
