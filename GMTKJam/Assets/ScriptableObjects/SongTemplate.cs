using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Song")]
public class SongTemplate : ScriptableObject
{
    public string songName;
    public Image songIcon;
    public AudioClip songFile;
}
