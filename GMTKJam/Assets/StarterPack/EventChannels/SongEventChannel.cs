using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Song Event Channel")]
public class SongEventChannel : GenericEventChannel<SongEvent> { }

[System.Serializable]
public struct SongEvent
{
    public SongTemplate SongValue;

    public SongEvent(SongTemplate song) => SongValue = song;
}
