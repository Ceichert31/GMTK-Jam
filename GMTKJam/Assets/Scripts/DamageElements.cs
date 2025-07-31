using UnityEngine;

public class DamageElements : MonoBehaviour
{
    [SerializeField]
    private int playerLayer;

    [SerializeField]
    private VoidEventChannel resetSongEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            //Reset to start
            resetSongEvent.CallEvent(new());
        }
    }
}
