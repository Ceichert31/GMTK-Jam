using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    private AudioSource source;

    [SerializeField]
    private VoidEventChannel endOfSongEvent;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!source.isPlaying)
        {
            //Send event
            endOfSongEvent.CallEvent(new());
        }
    }

    public void SetNewSong(SongEvent ctx)
    {
        source.clip = ctx.SongValue.songFile;
        source.Play();
    }
}
