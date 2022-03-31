using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneController : MonoBehaviour
{
    public GameObject helpPanel;
    public Button playButton;
    public Button helpButton;
    public Button quitGame;
    public Button closeHelpPanel;

    private void Awake()
    {
        //Add onclick listeners to buttons 
        playButton.onClick.AddListener(PlayButtonPressed);
        helpButton.onClick.AddListener(HelpButtonPressed);
        quitGame.onClick.AddListener(Quit);
        closeHelpPanel.onClick.AddListener(HelpButtonPressed);
    }

    public void Quit()
    {
        Application.Quit();
    }

    //Loads the game scene which should be at buildindex 1 
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void HelpButtonPressed() {

        //Check if helppanel is active and flip state
        if (!helpPanel.activeSelf) {
            helpPanel.SetActive(true);
        }
        else
        {
            helpPanel.SetActive(false);
        }
    }

}
