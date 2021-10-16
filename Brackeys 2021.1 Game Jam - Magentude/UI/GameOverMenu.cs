using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    public GameObject[] menuItems;
    public EventSystem eventSystem;

    public GameObject restartButton;
    public GameObject secretText;

    private void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            //Sets all menu items as active once the game is over
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].gameObject.SetActive(true);
            }
            //Sets default location for cursor on menu
            eventSystem.SetSelectedGameObject(restartButton);

            //Handles giving out secret hint
            if (RoundManager.Instance.round >= 10 && GameManager.Instance.difficulty != 1 && GameManager.Instance.difficulty <= 5) 
            {
                secretText.SetActive(true);
                GameManager.Instance.knowsSecret = true;
            }
        }
        else
        {
            //Disables all Game Over UI elements
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].gameObject.SetActive(false);
            }
            secretText.SetActive(false);
        }
    }

    public void Restart()
    {
        AudioDirector.Instance.ResetVolume();
        Time.timeScale = 1f;
        GameManager.Instance.curScore = 0;
        GameManager.Instance.gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        AudioDirector.Instance.ResetVolume();
        GameManager.Instance.gameOver = false;
        AudioDirector.Instance.FadeOut();
        SceneManager.LoadScene("MainMenu");
    }
}
