using UnityEngine;

public class DamageElements : MonoBehaviour
{
    [SerializeField]
    private int playerLayer;

    [SerializeField] LayerMask layerMask;

    [SerializeField]
    private BoolEventChannel resetSongEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("fekwopaijmefg");
        if (collision.gameObject.layer == 8)
        {
            //Reset to start
            resetSongEvent.CallEvent(new());
        }
    }
}
