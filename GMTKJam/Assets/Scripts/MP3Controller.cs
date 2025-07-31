using NaughtyAttributes;
using UnityEngine;

public class MP3Controller : MonoBehaviour
{
    [SerializeField]
    private BoolEventChannel skipSongEvent;
    private BoolEvent skipEvent;

    [SerializeField]
    private SongTemplate currentSong;

    private void Awake()
    {
        skipEvent = new();
    }

    [Button("Skip Forward")]
    private void SkipForward()
    {
        skipEvent.Value = true;
        skipSongEvent.CallEvent(skipEvent);
    }

    private bool isPaused;

    [Button("Pause")]
    private void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    [Button("Skip Backward")]
    private void SkipBackward()
    {
        skipEvent.Value = false;
        skipSongEvent.CallEvent(skipEvent);
    }
}
