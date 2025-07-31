using NaughtyAttributes;
using UnityEngine;

public class MP3Controller : MonoBehaviour
{
    [SerializeField]
    private BoolEventChannel skipSongEvent;
    private BoolEvent skipEvent;

    [SerializeField]
    private BoolEventChannel pauseSongEvent;
    private BoolEvent pauseEvent;

    private bool isPaused;

    private void Awake()
    {
        skipEvent = new();
        pauseEvent = new();
    }

    [Button("Skip Forward")]
    private void SkipForward()
    {
        if (isPaused)
            return;

        skipEvent.Value = true;
        skipSongEvent.CallEvent(skipEvent);
    }

    [Button("Pause")]
    private void Pause()
    {
        isPaused = !isPaused;

        pauseEvent.Value = isPaused;
        pauseSongEvent.CallEvent(pauseEvent);

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
        if (isPaused)
            return;

        skipEvent.Value = false;
        skipSongEvent.CallEvent(skipEvent);
    }
}
