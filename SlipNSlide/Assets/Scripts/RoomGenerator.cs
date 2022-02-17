using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject startRoom;
    public GameObject nextRoom;

    private GameObject lastRoom;


    void Start()
    {
        lastRoom = Instantiate(startRoom, new Vector3(0,0,0), Quaternion.identity);
        CreateFive();
    }

    void CreateFive()
    {
        Vector3 nextLoc = startRoom.transform.position;

        for (int i = 0; i <= 5; i++)
        {
            lastRoom = Instantiate(nextRoom, nextLoc, Quaternion.identity);

            nextLoc.y += 18;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

}
