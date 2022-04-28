using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public GameObject[] tutorialSteps;
    public int stepCounter = 0;
    public bool completed = false;
    private int shootCounter = 0;
    public bool didScrollWheelUp = false;
    public bool didScrollWheelDown = false;
    public bool didSpace = false;
    public bool didSwing = false;
    private bool swapShotgun = false;
    private bool swapAK = false;
    public GameObject skipStep;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (completed) return;

        if (Input.GetKeyDown(KeyCode.S)) {
            for (int i = 0; i < tutorialSteps.Length; i++) {
                tutorialSteps[i].SetActive(false);
			}
            skipStep.SetActive(false);
			completed = true;
            return;
		}

		for (int i = 0; i < tutorialSteps.Length; i++)
		{
			if (i == stepCounter)
			{
				tutorialSteps[i].SetActive(true);
			}
			else
			{
				tutorialSteps[i].SetActive(false);
			}
		}

        //step 1  - introduce shooting
        if (stepCounter == 0)
        {
            if (Input.GetMouseButtonDown(0)) shootCounter++;
            if (shootCounter >= 3) stepCounter++;
        }

        //setp 2 - hold grapple
        else if (stepCounter == 1)
        {
            //logic handled in grapple.cs
            shootCounter = 0;

        }
        //step 3 - scroll wheel, space, swing 
        else if (stepCounter == 2) {
            if (Input.GetMouseButtonUp(1)) {
                stepCounter = 1;
            }

            if (didScrollWheelDown && didScrollWheelUp && didSpace && didSwing) {
                stepCounter = 3;
            }

            if (Input.GetMouseButtonDown(0)) shootCounter++;
            if (shootCounter >= 3) didSwing = true;

            //weapon swap
        } else if (stepCounter == 3) {
            if (Input.GetKeyDown(KeyCode.Alpha2)) swapShotgun = true;
            if (Input.GetKeyDown(KeyCode.Alpha3)) swapAK = true;
            if (swapAK && swapShotgun) {
                stepCounter = 0;
                tutorialSteps[3].SetActive(false);
                skipStep.SetActive(false);
                completed = true;
            } 
        }


        //Debug.Log(stepCounter);
    }
}
