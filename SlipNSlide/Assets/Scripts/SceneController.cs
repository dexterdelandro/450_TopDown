using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;


public class SceneController : MonoBehaviour
{
    public static SceneController controller; //this is our single instance to be used

    public GameObject helpPanel;
    public Button playButton;
    public Button helpButton;
    public Button closeHelpPanel;
    public Button returnToHome;
    public Button endGameButton;
    private int currentSceneIndex;

    public void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentSceneIndex);

        if (currentSceneIndex == 0)
        {
            //Add onclick listeners to buttons 
            playButton.onClick.AddListener(PlayButtonPressed);
            helpButton.onClick.AddListener(HelpButtonPressed);
            closeHelpPanel.onClick.AddListener(HelpButtonPressed);
        }
        else if (currentSceneIndex == 2)
        {
            playButton.onClick.AddListener(PlayButtonPressed);
            returnToHome.onClick.AddListener(LoadMenu);
        }
        else
        {
            returnToHome.onClick.AddListener(LoadMenu);
            endGameButton.onClick.AddListener(LoadEnd);
            closeHelpPanel.onClick.AddListener(HelpButtonPressed);
        }


        if (controller != null) GameObject.Destroy(controller);
        else controller = this;
    }

    public void LoadMenu()
    {
        GameObject manager = GameObject.Find("Manager");
        if (manager) GameObject.Destroy(manager);
        SceneManager.LoadScene(0);
        
    }

    public void LoadEnd()
    {
        SceneManager.LoadScene(2);
    }

    //Loads the game scene which should be at buildindex 1 
    public void PlayButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void HelpButtonPressed()
    {

        //Check if helppanel is active and flip state
        if (!helpPanel.activeSelf)
        {
            helpPanel.SetActive(true);
        }
        else
        {
            helpPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
