using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayButton : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject playButton;
    public PrefabPlacementChecker placementChecker;  //  추가됨

    public void OnPlayClicked()
    {
        if (director != null)
            director.Play();

        if (placementChecker != null)
            placementChecker.MarkTimelinePlayed();  //  버튼 누르면 체크

        if (playButton != null)
            playButton.SetActive(false);
    }
}
