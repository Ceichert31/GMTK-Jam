using UnityEngine;

[System.Serializable]
public struct StatLine
{
    public float speed;
    public float jumpForce;
}

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    public int currentLevelIndex = 1;

    //InitalPlayer Stats
    [SerializeField] StatLine initalPlayerStats;

    //Modifier for changed player stats
    public StatLine enhancedPlayerStats;

    //Current Player Stats
    public StatLine currentPlayerStats;

    private void Awake()
    {
        instance = this;
        OnChangeLevel(new());
    }

    private void Start()
    {

    }

    public void OnChangeLevel(BoolEvent ctx)
    {
        ResetStats();

        AllocateStats();
    }

    private void AllocateStats()
    {
        switch (currentLevelIndex)
        {
            case 1:
                currentPlayerStats.jumpForce = 20f;
                break;
            //Speed Level
            case 2:
                currentPlayerStats.speed += enhancedPlayerStats.speed;
                break;
            //Jump Force
            case 3:
                currentPlayerStats.jumpForce += enhancedPlayerStats.jumpForce;
                break;
            case 4:
                break;
        }
    }

    public void IncreaseStats(float amount)
    {
        switch (currentLevelIndex)
        {
            case 1:
                break;
            //Speed Level
            case 2:
                currentPlayerStats.speed += amount;
                break;
            //Jump Force
            case 3:
                currentPlayerStats.jumpForce += amount;
                break;
            case 4:
                break;
        }
    }


    void ResetStats()
    {
        currentPlayerStats = initalPlayerStats;
    }
}