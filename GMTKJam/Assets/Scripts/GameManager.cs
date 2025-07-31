using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform PlayerPosition;

    private void Awake()
    {
        Instance = this;
    }
}
