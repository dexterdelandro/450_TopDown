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
        
        CreateFive();
    }

    void CreateFive()
    {
        nextLoc = lastRoom.transform.position;

        for (int i = 0; i <= 5; i++)
        {
            lastRoom = Instantiate(nextRoom, nextLoc, Quaternion.identity);

            nextLoc.y += 18;
        }
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(lastRoom.transform.position, player.transform.position));
        if (Vector3.Distance(lastRoom.transform.position, player.transform.position) <= 10)
        {
            nextLoc.y += 18;
            CreateFive();
        }
    }

}
