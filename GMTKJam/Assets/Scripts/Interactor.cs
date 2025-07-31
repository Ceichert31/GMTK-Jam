using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private int collectableLayer;

    [SerializeField]
    private int damageLayer;

    [SerializeField]
    private VoidEventChannel resetSongEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != collectableLayer)
            return;

        if (collision.gameObject.layer == damageLayer)
        {
            //Reset to start
            resetSongEvent.CallEvent(new());
        }

        if (collision.TryGetComponent(out ICollectable collectable))
        {
            collision.transform.parent = transform.GetChild(0);
            collision.transform.localPosition = Vector2.zero;
            collectable.Collect();
        }
    }
}
