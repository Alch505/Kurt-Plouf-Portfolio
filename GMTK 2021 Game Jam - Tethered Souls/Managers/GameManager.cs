using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    public Transform playerSpawn;

    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }

        if (player.transform.position.y <= -50) 
        {
            PlayerManager.instance.TakeDamage(25);

            player.transform.position = playerSpawn.position;
        }

        if (Input.GetButtonDown("Pause")) 
        {
            if (!PlayerManager.instance.isDead)
            {
                if (isPaused)
                {
                    isPaused = false;
                }
                else
                {
                    isPaused = true;
                }
            }
            else 
            {
                isPaused = false;
            }
        }
    }
}
