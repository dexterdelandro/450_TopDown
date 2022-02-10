using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float speed;


    private Transform player;
    private Vector2 destination;
    private Vector2 origin;

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
        transform.position += transform.forward * speed * Time.deltaTime;

        if(Vector2.Distance(origin, transform.position) >= 5 || Vector2.Distance(origin, transform.position) == 5)
        {
            RemoveBullet();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Untagged"))
        {
            RemoveBullet();
        }
    }

    void RemoveBullet()
    {
        Destroy(gameObject);
    }
}
