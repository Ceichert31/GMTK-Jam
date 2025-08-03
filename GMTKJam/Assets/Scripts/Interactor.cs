using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private int collectableLayer;

    [SerializeField]
    private Transform itemHolder;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == collectableLayer)
        {
            collision.transform.parent = itemHolder;
            collision.transform.DOLocalMove(Vector3.zero, 0.5f);
            collision.transform.DOScale(1.5f, 0.5f);
            //Play pickup sounds
        }

        //if (collision.TryGetComponent(out ICollectable collectable))
        //{
        //    collectable.Collect();
        //}

        //Repurposed for powerups as well
        if (collision.TryGetComponent(out IEnhancable enhancement))
        {
            audioSource.PlayOneShot(clip);
            enhancement.EnhancePlayer();
        }
    }
}
