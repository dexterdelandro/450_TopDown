using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject startRoom;
    public GameObject nextRoom;

    private GameObject temp;


    void Start()
    {
        temp = Instantiate(startRoom, new Vector3(0,0,0), Quaternion.identity);

        CreateFive();
    }

    void CreateFive()
    {
        Vector3 nextLoc = temp.transform.position;

        for (int i = 0; i <= 5; i++)
        {
            nextLoc.y += 18;
            temp = Instantiate(nextRoom, nextLoc, Quaternion.identity);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
