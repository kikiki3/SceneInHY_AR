using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroPanelManager : MonoBehaviour
{
    public GameObject introPanel;         // Canvas �� Panel ��ü
    public RawImage videoDisplay;         // RawImage ������Ʈ
    public Button playButton;             // ���� ���� ��ư
    public VideoPlayer videoPlayer;       // Main Camera�� ���� VideoPlayer
    public GameObject realPlayButton;     // ���� Play ��ư (ó���� ��Ȱ��ȭ)

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

