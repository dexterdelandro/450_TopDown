using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject startRoom;
    public GameObject[] rooms;

    public List<GameObject> activeRooms;

    private RoomScript compatable;

    private GameObject player; 
    private GameObject lastRoom;
    private GameObject nextRoom;
    private Vector3 nextLoc;


    void Start()
    {
        player = GameObject.Find("player");
        lastRoom = Instantiate(startRoom, new Vector3(0,0,0), Quaternion.identity);

        //CreateRooms();
    }

    //Create a number of rooms ahead of the previous
    void CreateRooms()
    {
        nextLoc = lastRoom.transform.position;

        for (int i = 0; i <= 5; i++)
        {
            //lastRoom = Instantiate(RoomSelector(), nextLoc, Quaternion.identity);
            activeRooms.Add(lastRoom);
        }
    }


    
    void Update()
    {
        //When player is in vicinity of the last room
        if (Vector3.Distance(lastRoom.transform.position, player.transform.position) <= 10)
        {
            nextLoc.y += 15;
            CreateRooms();
        }
    }

    /*
    private GameObject RoomSelector()
    {
        nextRoom = rooms[Random.Range(0, 4)];

        for(int i = 0; i <= compatable.top.Length; i++)
        {
            if (compatable.top[i] == nextRoom)
            {
                compatable = nextRoom.GetComponent<RoomScript>();
                return nextRoom;
            }
        };

        for (int i = 0; i <= compatable.left.Length; i++)
        {
            if (compatable.left[i] == nextRoom)
            {
                compatable = nextRoom.GetComponent<RoomScript>();
                return nextRoom;
            }
        };

        for (int i = 0; i <= compatable.right.Length; i++)
        {
            if (compatable.right[i] == nextRoom)
            {
                compatable = nextRoom.GetComponent<RoomScript>();
                return nextRoom;
            }
        };


        return nextRoom;
    }
    */

}
