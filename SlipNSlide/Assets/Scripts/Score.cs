using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    uint score;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GameObject.Find("TextScore").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";

    }

    public uint getScore(){
        return score;
    }

    public void addScore(uint value)
    {
        score += value;
    }
}
