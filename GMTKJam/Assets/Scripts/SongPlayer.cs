using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    private AudioSource source;

    [SerializeField]
    private VoidEventChannel endOfSongEvent;

    private bool isPaused;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!source.isPlaying && !isPaused && Application.isFocused)
        {
            //Send event
            endOfSongEvent.CallEvent(new());
        }
    }

    public void PauseSong(BoolEvent ctx)
    {
        isPaused = ctx.Value;

        if (isPaused)
        {
            source.Pause();
        }
        else
        {
            source.UnPause();
        }
    }

    public void SetNewSong(SongEvent ctx)
    {
        source.clip = ctx.SongValue.songFile;
        source.Play();
    }
}
