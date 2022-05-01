using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterManager : MonoBehaviour
{
    private int limit = 3;
    public GameObject[] clusters;
    void Start()
    {
        clusters = GameObject.FindGameObjectsWithTag("Cluster");

    }

    void Update()
    {
        clusters = GameObject.FindGameObjectsWithTag("Cluster");

        if(clusters.Length > limit)
        {
            foreach (GameObject cluster in clusters)
            {
                if(cluster.GetComponent<ClusterCheck>().completed == true)
                {
                    foreach (Transform child in cluster.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    Destroy(cluster.gameObject);
                }
            }
        }
    }
}
