using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    //Enemy Stats
    public float speed;
    public float health;
    public float fireRate;

    public float awareness;
    public float attackRange;


    //Player based info
    public Transform target;

    //Ammo type
    public GameObject bullet;

    private State currentState;
    private enum State
    {
        Casual,
        Battle
    }

    void Start()
    {
        currentState = State.Casual;
    }

    void Update()
    {
        switch (currentState)
        {
            //Stand still if not in combat
            case State.Casual:
                transform.position = this.transform.position;

                FindTarget();
                break;

            case State.Battle:
                //If player is out of shooting range, move in
                if (Vector2.Distance(transform.position, target.position) > attackRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                }
                //If player is within range, stop moving toward them
                else if (Vector2.Distance(transform.position, target.position) <= attackRange)
                {
                    transform.position = this.transform.position;

                    Attack();
                }
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collide with player bullets
        if (collision.CompareTag("Player"))
        {
            TakeDamage();
        }
    }

    void FindTarget()
    {
        if (Vector2.Distance(transform.position, target.position) < awareness)
        {
            currentState = State.Battle;
        }
    }

    void Attack()
    {
        if(fireRate <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            fireRate = 2;
        }
        else 
        {
            fireRate -= Time.deltaTime;
        }
    }

    void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
