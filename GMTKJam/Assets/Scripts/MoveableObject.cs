using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using TMPro;

public class MoveableObject : MonoBehaviour
{
    [SerializeField]
    private string objectID;

    [SerializeField]
    private float openDuration = 1f;

    [SerializeField]
    private Vector3 endPosition;

    [SerializeField]
    private TextMeshPro warningText;

    [SerializeField]
    private ParticleSystem particle;

    public string DoorID
    {
        get { return objectID; }
    }

    private void Start() { }

    [Button]
    public void MoveObject()
    {
        transform.DOMove(endPosition, openDuration).OnComplete(Cleanup);

        if (particle != null)
        {
            particle.Play();
        }
        //Camera shake
        CameraShakeManager.Instance.Shake(openDuration);
    }

    public void Cleanup()
    {
        if (particle != null)
        {
            particle.Stop();
        }
    }

    public void warningTextUp() 
    {
        warningText.text = "You need the " + DoorID + " iteam";
    }
}
