using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoTriggers : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public Button playButton;
    public Button replayButton;

    void Start()
    {
        rawImage.enabled = false; // 영상 화면 숨기기

        playButton.onClick.AddListener(PlayVideo);
        replayButton.onClick.AddListener(RestartVideo);
    }

    void PlayVideo()
    {
        rawImage.enabled = true;
        videoPlayer.Play();
    }

    void RestartVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Play();
    }
}
