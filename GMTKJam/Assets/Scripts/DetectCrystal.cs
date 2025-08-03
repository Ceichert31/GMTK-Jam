using UnityEngine;

public class DetectCrystal : MonoBehaviour, IResetable
{
    //Bass boost song after getting crystal

    [SerializeField]
    private VoidEventChannel completedMinigameEvent;

    [SerializeField]
    private int collectableLayer;

    private GameObject crystal;

    [SerializeField]
    private GameObject collectableCrystal;

    [SerializeField]
    private Transform originalPosition;

    private void Awake()
    {
        crystal = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == collectableLayer)
        {
            //Send event to remove song from loop
            //Minigame complete
            //Remove crystal

            collision.gameObject.SetActive(false);
            crystal.SetActive(true);

            completedMinigameEvent.CallEvent(new());
        }
    }

    public void Reset()
    {
        crystal.SetActive(false);
        collectableCrystal.SetActive(true);
        collectableCrystal.transform.parent = originalPosition;
        collectableCrystal.transform.localPosition = Vector3.zero;
    }
}
