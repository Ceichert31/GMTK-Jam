using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    private LockedDoor door;

    private void Start()
    {
        door = GetComponentInParent<LockedDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if player has key and open door

        door.OpenDoor();
    }
}
