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

    public int currentLevelIndex;

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
            case 5:
                break;
        }
    }

    void ResetStats()
    {
        currentPlayerStats = initalPlayerStats;
    }
}