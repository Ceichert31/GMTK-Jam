using UnityEngine;

/// <summary>
/// Plays a particle after the player interacts with it
/// </summary>
public class TriggerParticle : MonoBehaviour
{
    private ParticleSystem particle;

    [SerializeField]
    private bool destroyOnPlay;

    [SerializeField]
    private int playerLayer;

    private void Start()
    {
        particle = transform.parent.GetChild(1).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            particle.Play();

            if (destroyOnPlay)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
