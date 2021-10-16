using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public int comboCount;
    public int comboScore;

    public int difficulty; // 1 easy, 2 medium, 3 hard, 5 expert, 6 or higher Master

    public int curScore;

    public int[] highScores;

    public bool paused;
    public bool gameOver;

    public bool screenShake;

    public bool knowsSecret;

    void Update()
    {
        //Pausing the game and locking cursor
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            //Pausing the game
            if (Input.GetButtonDown("Pause"))
            {
                if (!gameOver)
                {
                    if (paused)
                    {
                        AudioDirector.Instance.ResetVolume();
                        AudioDirector.Instance.PlayPause(0);
                        paused = false;
                    }
                    else
                    {
                        AudioDirector.Instance.HalveVolume();
                        AudioDirector.Instance.PlayPause(1);
                        paused = true;
                    }
                }
            }

            if (!paused && !gameOver)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else 
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //Set Time Scale for game over or pause
        if (gameOver || paused) 
        {
            Time.timeScale = 0f;
        }
        else
        {
            StartCoroutine("Unpause");
        }
    }

    public void StartGameOver() 
    {
        AudioDirector.Instance.HalveVolume();
        gameOver = true;

        //Set highscores
        for (int i = 0; i < highScores.Length; i++) 
        {
            if (curScore > highScores[i]) 
            {
                int temp = highScores[i];

                highScores[i] = curScore;

                curScore = temp;
            }
        }

        comboCount = 0;
        comboScore = 0;
    }

    IEnumerator Unpause() 
    {
        if (paused) 
        {
            Time.timeScale = 1f;
        }
        StopCoroutine("Unpause");
        yield return new WaitForSeconds(0.1f);
    }
}
