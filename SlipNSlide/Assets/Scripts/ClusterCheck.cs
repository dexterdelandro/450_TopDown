using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterCheck : MonoBehaviour
{
    public bool completed;

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            if(child.Find("RoomBounds").GetComponent<RoomScript>().spawned == true)
            {
                completed = true;
            }
            else
            {
                completed = false;
            }
        }
    }
}
