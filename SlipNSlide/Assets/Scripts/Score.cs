using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public uint score;
    Text scoreText;

    public int health;
    public Image healthBar;


    Player playerScript;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GameObject.Find("TextScore").GetComponent<Text>();

        playerScript = GameObject.Find("player").GetComponent<Player>();
        health = playerScript.health;

        //make sure the script isnt destroyed when we go to the end screen
        // This way the final score will be accesible
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && GameObject.Find("Canvas"))
        {
            GameObject.Find("FinalScore").GetComponent<Text>().text = $"Final Score: {score}";
            Destroy(this);
            return;
        }

        scoreText.text = $"x {score}";

        health = playerScript.health;
        if(health <= 0)
        {
            healthBar.gameObject.SetActive(false);
            SceneController.controller.LoadEnd();
        }
        else
        {
            healthBar.gameObject.SetActive(true);
            healthBar.gameObject.transform.localScale = new Vector3(health * 1.0f / 100f * 1.48f, .28267f, 1);
            healthBar.gameObject.transform.localPosition = new Vector3(-74 * (100 - health) / 100, 0, 0);

            if (health > 50)
            {
                healthBar.color = Color.green;
            }
            else if (health > 25)
            {
                healthBar.color = Color.yellow;
            }
            else
            {
                healthBar.color = Color.red;
            }
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
