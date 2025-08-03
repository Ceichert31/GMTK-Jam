using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public bool tutorial1;
    public bool tutorial2;
    public bool tutorial3;
    public bool tutorial4;
    public bool tutorial5;

    public GameObject walking;
    public GameObject jump;

    public static TutorialController Instance;

    private void Awake()
    {
        Instance = this;
    }
}
