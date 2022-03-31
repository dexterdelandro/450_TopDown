using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public uint score;
    Text scoreText;

    uint health;
    Text healthText;

    Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GameObject.Find("TextScore").GetComponent<Text>();

        health = 100;
        healthText = GameObject.Find("TextHealth").GetComponent<Text>();

        playerScript = GameObject.Find("player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";

        healthText.text = $"Health: {playerScript.health}";

    }

    public uint getScore(){
        return score;
    }

    public void addScore(uint value)
    {
        score += value;
    }

    
}
