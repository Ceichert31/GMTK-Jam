using DG.Tweening;
using UnityEngine;

public class ArrowBlink : MonoBehaviour
{
    private SpriteRenderer arrowRenderer;

    private void Start()
    {
        arrowRenderer = GetComponent<SpriteRenderer>();

        //arrowRenderer
        //    .DOFade(0, 0.0f)
        //    .OnStepComplete(() => arrowRenderer.DOFade(1, 0.0f).SetDelay(0.3f))
        //    .SetLoops(-1);
    }
}
