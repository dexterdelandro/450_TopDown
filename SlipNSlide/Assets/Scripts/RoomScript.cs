using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    //Enemies and Doors
    public GameObject enemy;
    GameObject[] enemies;
    public bool spawned;
    public GameObject door;
    GameObject[] doors;

    //Rooms
    public GameObject[] top;
    public GameObject[] bottom;
    public GameObject[] left;
    public GameObject[] right;
    private Transform parent;

    private int rand;
    private RoomTemplates layout;

    private bool cleared;
    List<GameObject> spawns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent;
        spawned = false;

        layout = GameObject.FindGameObjectWithTag("Room Template").GetComponent<RoomTemplates>();
        PickLayout();
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
        for (int i = 0; i < layout.LayOuts[rand].transform.childCount; ++i)
        {
            Transform currentItem = layout.LayOuts[rand].transform.GetChild(i);
            
            if (currentItem.CompareTag("EnemySpawn"))
            {
                spawns.Add(currentItem.gameObject);
                continue;
            }
        }

        foreach (GameObject spots in spawns)
        {
            Instantiate(enemy, spots.transform.position + this.transform.position, Quaternion.identity);
        }
    }

    void SpawnDoors()
    {
        if(parent.name == "TB(Clone)")
        {
            Instantiate(door, transform.position + new Vector3(0, 8, 0), Quaternion.identity);
            Instantiate(door, transform.position + new Vector3(0, -8, 0), Quaternion.identity);
        }
        else if (parent.name == "T(Clone)")
        {
            Instantiate(door, transform.position + new Vector3(0, 8, 0), Quaternion.identity);
        }
        else if (parent.name == "TL(Clone)")
        {
            Instantiate(door, transform.position + new Vector3(0, 8, 0), Quaternion.identity);
            Instantiate(door, transform.position + new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 90));
        }
        else if (parent.name == "TR(Clone)")
        {
            Instantiate(door, transform.position + new Vector3(0, 8, 0), Quaternion.identity);
            Instantiate(door, transform.position + new Vector3(7, 0, 0), Quaternion.Euler(0, 0, 90));
        }
        else if(parent.name == "B(Clone)")
        {
            Instantiate(door, transform.position + new Vector3(0, -8, 0), Quaternion.identity);
        }
        else if (parent.name == "L(Clone)")
        {
            Instantiate(door, transform.position + new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 90));
        }
        else if (parent.name == "LR(Clone)")
        {
            Instantiate(door, transform.position + new Vector3(7, 0, 0), Quaternion.Euler(0, 0, 90));
            Instantiate(door, transform.position + new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 90));
        }
        else if (parent.name == "LRB(Clone)")
        {
            Instantiate(door, transform.position + new Vector3(0, -8, 0), Quaternion.identity);
            Instantiate(door, transform.position + new Vector3(7, 0, 0), Quaternion.Euler(0, 0, 90));
            Instantiate(door, transform.position + new Vector3(-7, 0, 0), Quaternion.identity);
        }
        else if (parent.name == "R(Clone)")
        {
            Instantiate(door, transform.position + new Vector3(7, 0, 0), Quaternion.Euler(0, 0, 90));
        }
        else if (parent.name == "RB(Clone)")
        {
            Instantiate(door, transform.position + new Vector3(0, -8, 0), Quaternion.identity);
            Instantiate(door, transform.position + new Vector3(7, 0, 0), Quaternion.Euler(0, 0, 90));
        }
    }

    void destroyDoors()
    {
       foreach (GameObject item in doors)
       {
           Destroy(item);
       }
    }

    void PickLayout()
    {
        rand = Random.Range(0, layout.LayOuts.Length);

        Instantiate(layout.LayOuts[rand], transform.position, Quaternion.identity);
    }
}
