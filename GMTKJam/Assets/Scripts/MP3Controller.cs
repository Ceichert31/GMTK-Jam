using NaughtyAttributes;
using UnityEngine;

public class MP3Controller : MonoBehaviour
{
    [SerializeField]
    private BoolEventChannel skipSongEvent;
    private BoolEvent skipEvent;

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

    [Button("Skip Backward")]
    private void SkipBackward()
    {
        skipEvent.Value = false;
        skipSongEvent.CallEvent(skipEvent);
    }
}
