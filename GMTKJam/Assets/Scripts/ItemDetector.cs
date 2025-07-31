using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    private MoveableObject door;

    private void Start()
    {
        door = GetComponentInParent<MoveableObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if player has key and open door
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            if (string.Equals(door.DoorID, collectable.ID))
            {
                collectable.Delete();
                door.MoveObject();
            }
        }
        else
        {
            door.WarningTextUp();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        door.WarningTextDown();
    }
}
