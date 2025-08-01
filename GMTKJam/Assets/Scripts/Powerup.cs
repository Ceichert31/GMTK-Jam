using UnityEngine;


/* TODO:
 * 
 * Make player stats public and modifyable
 * Make it so that the player stats are reset to what they were besides the modifier corresponding to each level(this will be done via some sort of array list)
 * Create seperate enhancement scripts for each needed enhancement
 * Finish creating speed level
 * 
 */
public class SpeedEnhancement : MonoBehaviour, IEnhancable
{
    public void EnhancePlayer()
    {
        
    }
}

public interface IEnhancable
{
    void EnhancePlayer();
}
