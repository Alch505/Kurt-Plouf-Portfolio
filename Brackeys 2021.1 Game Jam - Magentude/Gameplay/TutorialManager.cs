using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public bool waitingForInput;

    public bool canControlLS;
    public bool canControlRS;
    public bool canControlSwitching;
    public GameObject slowMotion;

    public GameObject tutorialText;
    public bool textIsOut;

    public GameObject buttonText;

    public Transform topAnchor;
    public Transform botAnchor;

    public bool readyForNext;
    public int step;

    public GameObject singleDummy;
    public GameObject tripleDummy;
    public GameObject[] triplets;
    public bool tripletsOut;

    void Start() 
    {
        Time.timeScale = 1f;
        step = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Handles disabling controls or waiting for input.
        #region
        if (waitingForInput)
        {
            buttonText.SetActive(true);

            //canControlLS = false;
            //canControlRS = false;
            //canControlSwitching = false;

            if (Input.GetButtonDown("Switch")) 
            {
                waitingForInput = false;
                
                buttonText.SetActive(false);

                if (readyForNext) 
                {
                    step += 1;
                    readyForNext = false;
                }
            }
        }

        if (canControlLS)
        {
            PlayerManager.Instance.LShip.GetComponent<Movement>().moveSpeed = 8;
        }
        else 
        {
            PlayerManager.Instance.LShip.GetComponent<Movement>().moveSpeed = 0;
        }

        if (canControlRS)
        {
            PlayerManager.Instance.RShip.GetComponent<Movement>().moveSpeed = 8;
        }
        else
        {
            PlayerManager.Instance.RShip.GetComponent<Movement>().moveSpeed = 0;
        }

        if (!canControlSwitching)
        {
            //slowMotion.SetActive(false);
            PlayerManager.Instance.connecting = false;
            PlayerManager.Instance.LShip.GetComponent<Movement>().comingTogether = false;
            PlayerManager.Instance.RShip.GetComponent<Movement>().comingTogether = false;
        }
        else 
        {
            slowMotion.SetActive(true);
        }
        #endregion

        //Handles moving the text
        if (textIsOut)
        {
            tutorialText.SetActive(true);
            //if (Vector3.Distance(tutorialText.transform.position, botAnchor.position) < 0.001f)
            //{
            //    transform.position = Vector3.Lerp(transform.position, botAnchor.position,
            //        Vector3.Distance(tutorialText.transform.position, botAnchor.position) * 2 * Time.deltaTime);
            //}
        }
        else 
        {
            tutorialText.SetActive(false);
            //if (Vector3.Distance(tutorialText.transform.position, topAnchor.position) < 0.001f)
            //{
            //    transform.position = Vector3.Lerp(transform.position, topAnchor.position,
            //        Vector3.Distance(tutorialText.transform.position, topAnchor.position) * 2 * Time.deltaTime);
            //}
        }

        //Handles which step is active
        switch (step)
        {
            case 1:
                StepOne();
                break;
            case 2:
                StepTwo();
                break;
            case 3:
                StepThree();
                break;
            case 4:
                StepFour();
                break;
            case 5:
                StepFive();
                break;
            case 6:
                StepSix();
                break;
            case 7:
                StepSeven();
                break;
            case 8:
                StepEight();
                break;
        }
    }

    public void StepOne()
    {
        
        textIsOut = true;
        tutorialText.GetComponent<Text>().text = "Hello and welcome to Magnetude!\n" +
            "In this tutorial you'll learn the basics of play.";
        //StartCoroutine(Delayer());
        waitingForInput = true;
        readyForNext = true;
    }

    public void StepTwo()
    {
        tutorialText.GetComponent<Text>().text = "The Left Stick or WASD controls the Red Ship.";
        canControlLS = true;
        //StartCoroutine("Delayer");
        waitingForInput = true;
        readyForNext = true;
    }

    public void StepThree()
    {
        tutorialText.GetComponent<Text>().text = "The Right Stick or Arrow Keys control the Blue Ship.";
        canControlRS = true;
        //StartCoroutine("Delayer");
        waitingForInput = true;
        readyForNext = true;
    }

    public void StepFour() 
    {
        tutorialText.GetComponent<Text>().text = "Pressing and releasing a Shoulder Button or Space " +
            "will turn on/off your magnets.\nThis will cause the two ships to fly together!\nHolding will give you a short window of bullet time.";
        slowMotion.SetActive(true);
        //StartCoroutine("DelayerForSwitching");
        waitingForInput = true;
        readyForNext = true;
    }

    public void StepFive()
    {
        tutorialText.GetComponent<Text>().text = "While the magnets are connected, you can move both ships with Left Stick or WASD." +
            "\nAnd you can disconnect them by pressing the Shoulder Buttons or Space again.";
        canControlSwitching = true;
        //StartCoroutine("Delayer");
        waitingForInput = true;
        readyForNext = true;
    }

    public void StepSix() 
    {
        tutorialText.GetComponent<Text>().text = "If you hit any enemy as your magnets fly together, it'll be destroyed!" +
            "\nWatchout though!\nYou can only safely touch enemies while your magnets are flying together.";
        if (singleDummy != null) 
        {
            singleDummy.SetActive(true);
        }
        else
        {
            //Debug.Log("Dummy Killed");
            waitingForInput = true;
            readyForNext = true;
        }
    }

    public void StepSeven() 
    {
        tutorialText.GetComponent<Text>().text = "If you hit more than one enemy as your magnets fly together you'll earn\n" +
            "a combo multiplier, netting you more points!";
        if (triplets[0] != null && triplets[1] != null && triplets[2] != null)
        {
            if (!tripletsOut) 
            {
                tripleDummy.SetActive(true);
                tripletsOut = true;
            }
        }
        if (triplets[0] == null && triplets[1] == null && triplets[2] == null)
        {
            //Debug.Log("Dummy Killed");
            waitingForInput = true;
            readyForNext = true;
        }
    }

    public void StepEight() 
    {
        tutorialText.GetComponent<Text>().text = "And with that, you're ready for action!\n" +
            "You can leave at any time by pressing the Menu Button or Escape to open the pause menu.";
    }

    //IEnumerator Delayer() 
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    Debug.Log("In Delayer");
    //    waitingForInput = true;
    //    //StopCoroutine("Delayer");
    //}
    //IEnumerator DelayerForSwitching()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    canControlSwitching = true;
    //    slowMotion.SetActive(true);
    //    waitingForInput = true;
    //    //StopCoroutine("Delayer");
    //}
}
