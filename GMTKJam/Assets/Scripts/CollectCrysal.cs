using DG.Tweening;
using UnityEngine;

public class CollectCrysal : MonoBehaviour, IResetable
{
    [SerializeField]
    private int playerLayer;

    [SerializeField]
    private GameObject arrow;

    private bool isEnabled = true;

    public void Reset()
    {
        arrow.transform.DOScaleY(0, 0f);
        DOTween.CompleteAll();
        isEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEnabled)
            return;

        if (collision.gameObject.layer == playerLayer)
        {
            isEnabled = false;
            arrow.transform.DOScaleY(18, 0.5f);
            CameraShakeManager.Instance.Shake(1f, 2f);
            GetComponent<AudioSource>().Play();
        }
    }
}
