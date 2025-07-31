using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField]
    private float openDuration = 1f;

    [SerializeField]
    private float openYPosition;

    [Button]
    public void OpenDoor()
    {
        transform.DOMoveY(openYPosition, openDuration);
        //transform.DOShakePosition(1f);
        //Camera shake
        //Camera.main.DOShakePosition(openDuration);
    }
}
