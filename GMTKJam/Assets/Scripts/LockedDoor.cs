using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField]
    private string doorID;

    [SerializeField]
    private float openDuration = 1f;

    [SerializeField]
    private float openYPosition;

    public string DoorID
    {
        get { return doorID; }
    }

    [Button]
    public void OpenDoor()
    {
        transform.DOMoveY(openYPosition, openDuration);
        //transform.DOShakePosition(1f);
        //Camera shake
        //Camera.main.DOShakePosition(openDuration);
    }
}
