using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public int openingDir;

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Room Template").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
        
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDir == 1)
            {
                rand = Random.Range(0, templates.bottomR.Length);
                Instantiate(templates.bottomR[rand], transform.position, Quaternion.identity);
            }
            else if (openingDir == 2)
            {
                rand = Random.Range(0, templates.topR.Length);
                Instantiate(templates.topR[rand], transform.position, Quaternion.identity);
            }
            else if (openingDir == 3)
            {
                rand = Random.Range(0, templates.leftR.Length);
                Instantiate(templates.leftR[rand], transform.position, Quaternion.identity);
            }
            else if (openingDir == 4)
            {
                rand = Random.Range(0, templates.rightR.Length);
                Instantiate(templates.rightR[rand], transform.position, Quaternion.identity);
            }

            spawned = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("RoomSpawn") && collision.GetComponent<SpawnPoints>().spawned == true)
        {
            Destroy(gameObject);
        }
    }
}
