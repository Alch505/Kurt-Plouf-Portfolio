using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] menuItems;

    public void Update()
    {
        //Activates or deactivates UI elements for the Pause Menu
        if (GameManager.Instance.paused)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].gameObject.SetActive(true);
            }
        }
        else 
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].gameObject.SetActive(false);
            }
        }
    }

    public void Resume() 
    {
        GameManager.Instance.paused = false;
        AudioDirector.Instance.PlayPause(0);
        AudioDirector.Instance.ResetVolume();
    }

    public void Restart() 
    {
        GameManager.Instance.comboCount = 0;
        GameManager.Instance.comboScore = 0;
        GameManager.Instance.curScore = 0;
        GameManager.Instance.paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioDirector.Instance.ResetVolume();
    }

    public void QuitGame()
    {
        GameManager.Instance.comboCount = 0;
        GameManager.Instance.comboScore = 0;
        GameManager.Instance.curScore = 0;
        GameManager.Instance.paused = false;
        
        SceneManager.LoadScene("MainMenu");
        AudioDirector.Instance.ResetVolume();
        AudioDirector.Instance.FadeOut();
    }
}
