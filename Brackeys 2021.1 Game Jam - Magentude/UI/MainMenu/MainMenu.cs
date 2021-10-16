using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] mainItems;
    public GameObject[] optionsItems;
    public GameObject[] hiscoreItems;

    public EventSystem eventSystem;

    public string state;

    private int secretDif;
    public GameObject secretTip;

    private void Update()
    {
        //State Detection
        #region
        //if (state == "main")
        //{
        //    for (int i = 0; i < mainItems.Length; i++)
        //    {
        //        mainItems[i].gameObject.SetActive(true);
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < mainItems.Length; i++)
        //    {
        //        mainItems[i].gameObject.SetActive(false);
        //    }
        //}

        //if (state == "options")
        //{
        //    for (int i = 0; i < optionsItems.Length; i++)
        //    {
        //        optionsItems[i].gameObject.SetActive(true);
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < optionsItems.Length; i++)
        //    {
        //        optionsItems[i].gameObject.SetActive(false);
        //    }
        //}

        //if (state == "hiscores")
        //{
        //    for (int i = 0; i < hiscoreItems.Length; i++)
        //    {
        //        hiscoreItems[i].gameObject.SetActive(true);
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < hiscoreItems.Length; i++)
        //    {
        //        hiscoreItems[i].gameObject.SetActive(false);
        //    }
        //}
        #endregion

        if (GameManager.Instance.knowsSecret) 
        {
            secretTip.SetActive(true);
        }
    }

    //All
    #region
    public void ChangeScreen(GameObject selected) 
    {
        eventSystem.SetSelectedGameObject(selected);
    }

    public void Back()
    {
        state = "main";
    }
    #endregion

    //MainMenu
    #region
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        AudioDirector.Instance.FadeOut();
    }

    public void PlayTutorial() 
    {
        SceneManager.LoadScene("Tutorial");
        AudioDirector.Instance.FadeOut();
    }

    public void Options() 
    {
        state = "options";
    }

    public void HighScores() 
    {
        state = "hiscores";
    }

    public void QuitGame() 
    {
        Application.Quit();
        AudioDirector.Instance.FadeOut();
    }
    #endregion

    //Options
    #region
    public void Easy() 
    {
        GameManager.Instance.difficulty = 1;
        secretDif = 0;
    }

    public void Medium()
    {
        GameManager.Instance.difficulty = 2;
        secretDif = 0;
    }

    public void Hard()
    {
        GameManager.Instance.difficulty = 3;
        secretDif = 0;
    }

    public void Expert()
    {
        GameManager.Instance.difficulty = 5;
        if (secretDif <= 3) 
        {
            secretDif += 1;
        }
        else 
        {
            secretDif = 0;
            GameManager.Instance.difficulty = 15;
        }
    }

    public void MusicVolume(float newVolume) 
    {
        AudioDirector.Instance.musicVolume = newVolume * 0.01f;
    }
    public void SFXVolume(float newVolume)
    {
        AudioDirector.Instance.sfxVolume = newVolume * 0.01f;
    }

    public void ScreenShake() 
    {
        if (GameManager.Instance.screenShake)
        {
            GameManager.Instance.screenShake = false;
        }
        else 
        {
            GameManager.Instance.screenShake = true;
        }
    }

    public void ResetHighScores() 
    {
        for (int i = 0; i < GameManager.Instance.highScores.Length; i++) 
        {
            GameManager.Instance.highScores[i] = 0;
        }
    }
    #endregion
}
