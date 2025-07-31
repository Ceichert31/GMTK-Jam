using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField]
    private string objectID;

    [SerializeField]
    private float openDuration = 1f;

    [SerializeField]
    private Vector3 endPosition;

    [SerializeField]
    private TextMeshProUGUI warningText;

    [SerializeField]
    private float textPopupSpeed = 0.25f;

    [SerializeField]
    private ParticleSystem particle;

    private bool hasParticle;
    private bool hasText;

    public string DoorID
    {
        get { return objectID; }
    }

    private void Start()
    {
        hasParticle = particle;
        hasText = warningText;
    }

    [Button]
    public void MoveObject()
    {
        transform.DOMove(endPosition, openDuration).OnComplete(Cleanup);

        if (hasParticle)
        {
            particle.Play();
        }

        if (hasText)
        {
            warningText.text = string.Empty;
        }

        //Camera shake
        CameraShakeManager.Instance.Shake(openDuration);
    }

    public void Cleanup()
    {
        if (hasParticle)
        {
            particle.Stop();
        }
    }

    public void WarningTextUp()
    {
        if (!hasText)
            return;

        warningText.transform.DOScaleY(1f, textPopupSpeed);
        warningText.text = "You need the " + DoorID + " item";
    }

    public void WarningTextDown()
    {
        if (!hasText)
            return;

        warningText.transform.DOScaleY(0f, textPopupSpeed);
        warningText.text = string.Empty;
    }
}
