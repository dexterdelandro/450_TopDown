using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float speed;


    private Transform player;
    private Vector3 destination;
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        destination = new Vector2(player.position.x, player.position.y);
        origin = transform.position;
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
            collision.gameObject.GetComponent<Player>().health -= 10;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
            return;
        }


        Destroy(gameObject);
    }
}
