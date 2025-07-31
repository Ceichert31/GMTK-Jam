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
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            if (string.Equals(door.DoorID, collectable.ID))
            {
                collectable.Delete();
                door.OpenDoor();
            }
        }
    }
}
