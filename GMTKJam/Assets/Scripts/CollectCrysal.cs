using DG.Tweening;
using UnityEngine;

public class CollectCrysal : MonoBehaviour, IResetable
{
    [SerializeField]
    private int playerLayer;

    [SerializeField]
    private GameObject arrow;

    public void Reset()
    {
        arrow.transform.DOScaleY(0, 0f);
        DOTween.CompleteAll();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            arrow.transform.DOScaleY(18, 0.5f);
            CameraShakeManager.Instance.Shake(1f, 2f);
            GetComponent<AudioSource>().Play();
        }
    }
}
