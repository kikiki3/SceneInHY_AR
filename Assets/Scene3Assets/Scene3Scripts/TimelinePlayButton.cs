using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayButton : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject playButton;
    public PrefabPlacementChecker placementChecker;  //  �߰���

    public void OnPlayClicked()
    {
        if (director != null)
            director.Play();

        if (placementChecker != null)
            placementChecker.MarkTimelinePlayed();  //  ��ư ������ üũ

        if (playButton != null)
            playButton.SetActive(false);
    }
}
