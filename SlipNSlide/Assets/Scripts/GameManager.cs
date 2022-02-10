using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Score scoreScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreScript = gameObject.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
