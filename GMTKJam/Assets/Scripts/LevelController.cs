using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Controls which level the player is on and plays the according song
/// </summary>
public class LevelController : MonoBehaviour
{
    //Manage the players current level and move player between levels
    //We will need a list of all levels and their spawn points as well as the number of levels
    //We can use modulus to prevent the players current level from going out of scope

    [Header("Level Settings")]
    [SerializeField]
    private int currentLevel = 0;
    
    private int currentLevelIndex;

    [SerializeField] private List<int> currentLevels = new List<int>();

    [SerializeField]
    private List<SongLevelPair> levelSpawnpointList = new();

    [SerializeField]
    private float loopLength;

    [SerializeField]
    private float currentTime;

    [SerializeField]
    private SongEventChannel switchSongEvent;
    private SongEvent songEvent;

    private Vector3 noDamping = new(0, 0, 0);
    private Vector3 damping = new(1, 1, 1);
    private CinemachineFollow follow;

    private void Awake()
    {
        follow = Camera.main.GetComponent<CinemachineFollow>();

        SetupLevel(0);

        currentLevels.Add(1);
        currentLevels.Add(2);
        currentLevels.Add(3);
        currentLevels.Add(4);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }

    public void EndOfSong(VoidEvent ctx)
    {
        ChangeLevel(new());
    }

    /// <summary>
    /// Advances to the next level
    /// </summary>
    /// <remarks>
    /// Called by an event listener
    /// </remarks>
    /// <param name="ctx"></param>
    public void ChangeLevel(BoolEvent ctx)
    {
        if (Time.timeScale == 0)
            return;


        //Change level properly
        if(currentLevelIndex + 1 >= currentLevels.Count)
        {
            currentLevelIndex = 0;
        }
        else
        {
            currentLevelIndex++;
        }
        currentLevel = currentLevels[currentLevelIndex];
        StatsManager.instance.currentLevelIndex = currentLevel;

        StatsManager.instance.OnChangeLevel(new());

        Debug.Log(currentLevelIndex);
        SetupLevel(currentLevelIndex);

        if (currentLevel == levelSpawnpointList.Count - 1)
        {
            AlbumController.Instance.CheckCollected();
        }

        //Reset dropper scripts

        //Update UI
    }

    public void CompletedSong(VoidEvent ctx)
    {
        //Remove song from playlist and flag this one as complete
        Debug.Log(levelSpawnpointList[currentLevelIndex]);
        levelSpawnpointList.RemoveAt(currentLevelIndex);
        currentLevels.RemoveAt(currentLevelIndex);

        switch (currentLevelIndex)
        {
            case 0:
                AlbumController.Instance.hasAlbumOne = true;
                break;
            case 1:
                AlbumController.Instance.hasAlbumTwo = true;
                break;
            case 2:
                AlbumController.Instance.hasAlbumThree = true;
                break;
            case 3:
                AlbumController.Instance.hasAlbumFour = true;
                break;
        }

        //Send signal that level was complete
        currentLevelIndex--;
        ChangeLevel(new());
    }

    /// <summary>
    /// Sets all variables to initialize a level
    /// </summary>
    /// <param name="level"></param>
    private void SetupLevel(int level)
    {
        currentTime = 0;

        //Switch to current level
        GameManager.Instance.PlayerPosition.position = levelSpawnpointList[level]
            .spawnPoint
            .position;

        //Send event to switch songs
        songEvent.SongValue = levelSpawnpointList[level].Song;
        switchSongEvent.CallEvent(songEvent);
        loopLength = levelSpawnpointList[level].Song.songFile.length;

        follow.TrackerSettings.PositionDamping = noDamping;
        Invoke(nameof(ResetDamping), 0.1f);

        foreach (GameObject script in levelSpawnpointList[level].resetList)
        {
            if (script.TryGetComponent(out IResetable resetable))
            {
                resetable.Reset();
            }
        }
    }

    private void ResetDamping() => follow.TrackerSettings.PositionDamping = damping;
}

[System.Serializable]
public struct SongLevelPair
{
    public SongTemplate Song;
    public Transform spawnPoint;
    public List<GameObject> resetList;
}
