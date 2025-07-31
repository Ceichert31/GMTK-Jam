using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField]
    private string objectID;

    [SerializeField]
    private float openDuration = 1f;

    [SerializeField]
    private Vector3 endPosition;

    public string DoorID
    {
        get { return objectID; }
    }

    [Button]
    public void MoveObject()
    {
        transform.DOMove(endPosition, openDuration);
        //transform.DOShakePosition(1f);
        //Camera shake
        //Camera.main.DOShakePosition(openDuration);
    }
}
