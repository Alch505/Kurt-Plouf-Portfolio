using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private bool hasLoaded;

    void Start()
    {
        GameManager gameManager = GameManager.Instance;
        AudioDirector audioDirector = AudioDirector.Instance;

        //Load data
        if (!hasLoaded) 
        {
            //Scores
            gameManager.highScores[0] = PlayerPrefs.GetInt("Hi1st", 0);
            gameManager.highScores[1] = PlayerPrefs.GetInt("Hi2nd", 0);
            gameManager.highScores[2] = PlayerPrefs.GetInt("Hi3rd", 0);
            gameManager.highScores[3] = PlayerPrefs.GetInt("Hi4th", 0);
            gameManager.highScores[4] = PlayerPrefs.GetInt("Hi5th", 0);
            gameManager.highScores[5] = PlayerPrefs.GetInt("Hi6th", 0);
            gameManager.highScores[6] = PlayerPrefs.GetInt("Hi7th", 0);
            gameManager.highScores[7] = PlayerPrefs.GetInt("Hi8th", 0);
            gameManager.highScores[8] = PlayerPrefs.GetInt("Hi9th", 0);
            gameManager.highScores[9] = PlayerPrefs.GetInt("Hi10th", 0);

            //Screen Shake
            if (PlayerPrefs.GetString("Shake", "On") == "Off")
            {
                gameManager.screenShake = false;
            }
            else if (PlayerPrefs.GetString("Shake", "On") == "On") 
            {
                gameManager.screenShake = true;
            }

            //Audio
            audioDirector.musicVolume = PlayerPrefs.GetFloat("MVolume", 0.75f);
            audioDirector.sfxVolume = PlayerPrefs.GetFloat("SVolume", 0.75f);

            //Difficulty
            gameManager.difficulty = PlayerPrefs.GetInt("Difficulty", 2);
            if (PlayerPrefs.GetString("Secret", "No") == "Yes")
            {
                gameManager.knowsSecret = true;
            }
            else if (PlayerPrefs.GetString("Secret", "No") == "No") 
            {
                gameManager.knowsSecret = false;
            }
        }
        //Catch for if somehow it tries to load again
        else 
        {
            Debug.Log("Hi Score data has already loaded!");
        }
        hasLoaded = true;
    }

    //Save data
    private void OnApplicationQuit()
    {
        GameManager gameManager = GameManager.Instance;
        AudioDirector audioDirector = AudioDirector.Instance;

        //Scores
        PlayerPrefs.SetInt("Hi1st", gameManager.highScores[0]);
        PlayerPrefs.SetInt("Hi2nd", gameManager.highScores[1]);
        PlayerPrefs.SetInt("Hi3rd", gameManager.highScores[2]);
        PlayerPrefs.SetInt("Hi4th", gameManager.highScores[3]);
        PlayerPrefs.SetInt("Hi5th", gameManager.highScores[4]);
        PlayerPrefs.SetInt("Hi6th", gameManager.highScores[5]);
        PlayerPrefs.SetInt("Hi7th", gameManager.highScores[6]);
        PlayerPrefs.SetInt("Hi8th", gameManager.highScores[7]);
        PlayerPrefs.SetInt("Hi9th", gameManager.highScores[8]);
        PlayerPrefs.SetInt("Hi10th", gameManager.highScores[9]);

        //Screen Shake
        if (gameManager.screenShake)
        {
            PlayerPrefs.SetString("Shake", "On");
        }
        else 
        {
            PlayerPrefs.SetString("Shake", "Off");
        }

        //Audio
        PlayerPrefs.SetFloat("MVolume", audioDirector.musicVolume);
        PlayerPrefs.SetFloat("SVolume", audioDirector.sfxVolume);

        //Difficulty
        PlayerPrefs.SetInt("Difficulty", gameManager.difficulty);
        if (gameManager.knowsSecret) 
        {
            PlayerPrefs.SetString("Secret", "Yes");
        }
        else if (!gameManager.knowsSecret)
        {
            PlayerPrefs.SetString("Secret", "No");
        }
    }
}
