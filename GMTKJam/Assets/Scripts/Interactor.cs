using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private int collectableLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != collectableLayer)
            return;

        if (collision.TryGetComponent(out ICollectable collectable))
        {
            collision.transform.parent = transform.GetChild(0);
            collision.transform.localPosition = Vector2.zero;
            collectable.Collect();
        }

        //Repurposed for powerups as well
        if(collision.TryGetComponent(out IEnhancable enhancement))
        {
            enhancement.EnhancePlayer();
        }
    }
}
