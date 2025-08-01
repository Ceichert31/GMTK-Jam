using System;
using DG.Tweening;
using UnityEngine;

public class BreakableSurface : MonoBehaviour
{
    [SerializeField]
    private int playerLayer;

    private float health = 5;

    [SerializeField]
    private float bounceHeight = 15;

    [SerializeField]
    private GameObject damageParticle;

    [SerializeField]
    private ParticleSystem breakParticle;

    public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != playerLayer)
            return;

        //Rework to get speed before colliding
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        float speed = rb.linearVelocity.magnitude;
        damage = speed;
        health -= speed;

        if (health <= 0)
        {
            breakParticle.Play();
            //Break
            Destroy(gameObject);
            return;
        }

        Instantiate(damageParticle, (Vector3)collision.contacts[0].point, Quaternion.identity);
        rb.AddForceY(bounceHeight, ForceMode2D.Impulse);

        //Send player upwards if not fully broken
        //decrease health and break
    }
}
