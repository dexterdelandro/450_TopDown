using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterManager : MonoBehaviour
{
    public GameObject[] clusters;
    void Start()
    {
        clusters = GameObject.FindGameObjectsWithTag("Cluster");

        foreach (GameObject cluster in clusters)
        {
            Debug.Log(cluster.transform.position);
        }
    }

    void Update()
    {
        clusters = GameObject.FindGameObjectsWithTag("Cluster");
    }
}
