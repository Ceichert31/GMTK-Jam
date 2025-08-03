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

    [SerializeField] int currentLevelIndex;

    //InitalPlayer Stats
    [SerializeField] StatLine initalPlayerStats;

    //Modifier for changed player stats
    public StatLine enhancedPlayerStats;

    //Current Player Stats
    public StatLine currentPlayerStats;

    private void Awake()
    {
        instance = this;
        ResetStats();
        OnChangeLevel(new BoolEvent());
    }

    private void Start()
    {

    }

    public void OnChangeLevel(BoolEvent ctx)
    {
        //Makes the level index loop back around
        if (currentLevelIndex == 4)
        {
            currentLevelIndex = 1;
        }
        else
        {
            currentLevelIndex++;
        }

        ResetStats();

        IncreaseStats();
    }

    public void UpdateStats()
    {
        IncreaseStats();
    }

    void IncreaseStats()
    {
        switch (currentLevelIndex)
        {
            //Jump Force
            case 1:
                currentPlayerStats.jumpForce += enhancedPlayerStats.jumpForce;
                break;
            //Speed Level
            case 2:
                currentPlayerStats.speed += enhancedPlayerStats.speed;
                break;
            case 3:
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