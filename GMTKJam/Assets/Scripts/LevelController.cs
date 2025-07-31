using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Manage the players current level and move player between levels
    //We will need a list of all levels and their spawn points as well as the number of levels
    //We can use modulus to prevent the players current level from going out of scope

    [Header("Level Settings")]
    [SerializeField]
    private int currentLevel = 0;

    [SerializeField]
    private List<Transform> levelSpawnpointList = new();

    /// <summary>
    /// Switches levels based on the value passed through
    /// </summary>
    /// <remarks>
    /// Called by an event listener
    /// </remarks>
    /// <param name="ctx"></param>
    public void ChangeLevel(BoolEvent ctx)
    {
        if (ctx.Value)
        {
            //Move right
            //Prevent current level from going out of scope (rolls over to 0)
            currentLevel = currentLevel++ % levelSpawnpointList.Count;
        }
        else
        {
            //Move left
            currentLevel = currentLevel-- % levelSpawnpointList.Count;
        }

        //Switch to current level
    }
}
