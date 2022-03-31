using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Score score;

    public float speed = .5f;
    private float damage = 1f;

    private void Start()
    {
        score = GameObject.Find("Manager").GetComponent<Score>();
    }
    public void CreateBullet(Vector2 direction) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        //Debug.Log(direction);
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"{collision.gameObject.tag}");

        if (collision.gameObject.tag == "enemyBullet")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "playerBullet")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<enemyScript>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    public void SetDamage(float d)
    {
        damage = d;
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }
}
