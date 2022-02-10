using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    uint score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public uint getScore(){
        return score;
    }

    public void addScore(uint value)
    {
        score += value;
    }
}
