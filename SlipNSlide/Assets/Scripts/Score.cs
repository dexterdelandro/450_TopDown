using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    uint score;
    Text scoreText;

    uint health;
    Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GameObject.Find("TextScore").GetComponent<Text>();

        health = 100;
        healthText = GameObject.Find("HealthScore").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";

        healthText.text = $"Health: {health}";

    }

    public uint getScore(){
        return score;
    }

    public void addScore(uint value)
    {
        score += value;
    }

    public uint getHealth()
    {
        return health;
    }

    public void damage(uint value)
    {
        health -= value;
    }
}
