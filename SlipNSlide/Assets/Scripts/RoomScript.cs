using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public GameObject enemy;
    GameObject[] enemies;
    public bool spawned;
    public GameObject door;
    GameObject[] doors;

    public GameObject[] top;
    public GameObject[] bottom;
    public GameObject[] left;
    public GameObject[] right;

    private bool cleared;
    private Vector3 enemyspawn1;
    private Vector3 enemyspawn2;

    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
        enemyspawn1 = new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y + 5, 0);
        enemyspawn2 = new Vector3(gameObject.transform.position.x - 5, gameObject.transform.position.y + 5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if room is cleared
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        doors = GameObject.FindGameObjectsWithTag("Door");

        if (enemies.Length == 0)
        {
            cleared = true;
            destroyDoors();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Spawn enemies when player enters
        if (other.tag == "Player" && spawned == false)
        {
            cleared = false;
            SpawnDoors();
            SpawnEnemies();
            spawned = true;
        }
    }

    void SpawnEnemies()
    {
        Instantiate(enemy, enemyspawn1, Quaternion.identity);
        Instantiate(enemy, enemyspawn2, Quaternion.identity);
    }

    void SpawnDoors()
    {
            Instantiate(door, transform.position + new Vector3(0, 8, 0), Quaternion.identity);
            Instantiate(door, transform.position + new Vector3(0, -8, 0), Quaternion.identity);
            Instantiate(door, transform.position + new Vector3(8, 0, 0), Quaternion.Euler(0, 0, 90));
            Instantiate(door, transform.position + new Vector3(-8, 0, 0), Quaternion.Euler(0, 0, 90));
        
    }

    void destroyDoors()
    {
       foreach (GameObject item in doors)
       {
           Destroy(item);
       }
    }
}
