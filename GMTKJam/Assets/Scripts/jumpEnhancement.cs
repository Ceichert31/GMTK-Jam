using UnityEngine;

public class jumpEnhancement : MonoBehaviour, IEnhancable
{
    [SerializeField] float speedIncreaseAmount;
    public void EnhancePlayer()
    {
        Debug.Log("triggered");
        StatsManager.instance.enhancedPlayerStats.jumpForce += speedIncreaseAmount;
        StatsManager.instance.IncreaseStats(speedIncreaseAmount);
        GameObject.Destroy(gameObject);
    }
}


