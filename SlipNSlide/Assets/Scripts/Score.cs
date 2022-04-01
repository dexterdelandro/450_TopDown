using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        //make sure the script isnt destroyed when we go to the end screen
        // This way the final score will be accesible
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";

        healthText.text = $"Health: {playerScript.health}";

        if (playerScript.health <= 0 )
        {
            SceneController.controller.EndScene();
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 && GameObject.Find("Canvas"))
        {
            GameObject.Find("FinalScore").GetComponent<Text>().text = $"Final Score: {score}";
        }

    }

    public uint getScore()
    {
        return score;
    }

    public void addScore(uint value)
    {
        score += value;
    }


}
