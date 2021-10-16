using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused)
        {
            pauseMenu.SetActive(true);
        }
        else 
        {
            pauseMenu.SetActive(false);
        }

        if (PlayerManager.instance.isDead)
        {
            gameOverMenu.SetActive(true);
        }
        else
        {
            gameOverMenu.SetActive(false);
        }
    }

    public void Unpause() 
    {
        GameManager.instance.isPaused = false;
    }

    public void Restart() 
    {
        SceneManager.LoadScene("Test");
    }
}
