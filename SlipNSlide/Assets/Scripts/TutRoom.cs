using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutRoom : MonoBehaviour
{
    // Start is called before the first frame update

    public Tutorial starter;
    public GameObject startBlock;

    void Start()
    {
        starter = Tutorial.FindObjectOfType<Tutorial>();
        startBlock = Instantiate(startBlock, transform.position + new Vector3(0, 8, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(starter.completed == true)
        {
            Destroy(startBlock);
        }
    }
}
