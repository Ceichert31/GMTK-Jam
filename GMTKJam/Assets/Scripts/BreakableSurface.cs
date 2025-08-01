using System;
using DG.Tweening;
using UnityEngine;

public class BreakableSurface : MonoBehaviour
{
    [SerializeField]
    private int playerLayer;

    private int health = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != playerLayer)
            return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        float speed = rb.linearVelocity.magnitude;
        speed = Mathf.RoundToInt(speed);
        speed = Mathf.Clamp(speed, 1, UInt64.MaxValue);
        health -= (int)speed;

        if (health <= 0)
        {
            //Break
            Destroy(gameObject);

            return;
        }

        rb.AddForceY(100, ForceMode2D.Impulse);
        collision.gameObject.transform.DOShakePosition(0.3f);

        //Send player upwards if not fully broken
        //decrease health and break
    }
}
