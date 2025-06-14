using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineBinder : MonoBehaviour
{
    public PlayableDirector director;

    public GameObject chansung;
    public GameObject ghost;
    public GameObject manwol;

    public void BindAllCharacters()
    {
        foreach (var track in director.playableAsset.outputs)
        {
            if (track.streamName.Contains("chansung"))
                director.SetGenericBinding(track.sourceObject, chansung.GetComponent<Animator>());
            else if (track.streamName.Contains("ghost"))
                director.SetGenericBinding(track.sourceObject, ghost.GetComponent<Animator>());
            else if (track.streamName.Contains("manwol"))
                director.SetGenericBinding(track.sourceObject, manwol.GetComponent<Animator>());
        }
    }

    public void PlayTimeline()
    {
        BindAllCharacters();
        director.Play();
    }
}

