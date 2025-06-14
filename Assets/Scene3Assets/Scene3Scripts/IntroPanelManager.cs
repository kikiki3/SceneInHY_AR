using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroPanelManager : MonoBehaviour
{
    public GameObject introPanel;         // Canvas 안 Panel 전체
    public RawImage videoDisplay;         // RawImage 컴포넌트
    public Button playButton;             // 비디오 시작 버튼
    public VideoPlayer videoPlayer;       // Main Camera에 붙은 VideoPlayer
    public GameObject realPlayButton;     // 최종 Play 버튼 (처음엔 비활성화)

    void Start()
    {
        introPanel.SetActive(true);
        videoDisplay.gameObject.SetActive(false);
        realPlayButton.SetActive(false);

        playButton.onClick.AddListener(PlayIntroVideo);
    }

    void PlayIntroVideo()
    {
        playButton.gameObject.SetActive(false);
        videoDisplay.gameObject.SetActive(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        introPanel.SetActive(false);
        realPlayButton.SetActive(true);
    }
}

