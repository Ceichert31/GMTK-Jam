using UnityEngine;

public class BreakableSurface : MonoBehaviour
{
    [SerializeField]
    private int playerLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            //Send player upwards if not fully broken
            //decrease health and break
        }
    }
}
