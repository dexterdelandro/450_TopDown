using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject startRoom;
    public GameObject nextRoom;


    private GameObject player; 
    private GameObject lastRoom;
    private Vector3 nextLoc;


    void Start()
    {
        player = GameObject.Find("player");
        lastRoom = Instantiate(startRoom, new Vector3(0,0,0), Quaternion.identity);
        
        CreateRooms();
    }

    //Create a number of rooms ahead of the previous
    void CreateRooms()
    {
        nextLoc = lastRoom.transform.position;

        for (int i = 0; i <= 5; i++)
        {
            lastRoom = Instantiate(nextRoom, nextLoc, Quaternion.identity);

            nextLoc.y += 18;
        }
    }


    
    void Update()
    {
        //When player is in vicinity of the last room
        if (Vector3.Distance(lastRoom.transform.position, player.transform.position) <= 10)
        {
            nextLoc.y += 18;
            CreateRooms();
        }
    }

}
