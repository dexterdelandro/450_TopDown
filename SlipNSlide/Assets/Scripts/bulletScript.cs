using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float speed;
    private int damage;


    private Transform player;
    public Vector3 destination;
    public Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (destination - origin).normalized * speed * Time.deltaTime;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"Enemy bullet collided with {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().health -= damage;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
            return;
        }
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.gameObject.GetComponent<CircleCollider2D>());
            return;
        }


        Destroy(gameObject);
    }

    public void SetDamage(int d)
    {
        damage = d;
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void SetRotation(Vector3 target)
    {
        origin = transform.position;
        destination = origin + target;
    }
}
