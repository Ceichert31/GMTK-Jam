using DG.Tweening;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float duration, float intensity = 1)
    {
        transform.DOShakePosition(duration, intensity);
    }
}
