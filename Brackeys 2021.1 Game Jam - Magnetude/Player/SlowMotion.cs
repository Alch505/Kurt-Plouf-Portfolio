using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 1.5f;

    void Update()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        //Makes sure game is not paused or in game over states
        if (!GameManager.Instance.gameOver && !GameManager.Instance.paused) 
        {
            //Dectects if the button is pressed or released to cause slomo
            if (Input.GetButtonDown("Switch"))
            {
                if (!PlayerManager.Instance.shipsConnected && !PlayerManager.Instance.connecting)
                {
                    //AudioDirector.Instance.PlaySlowDown();
                    DoSlowMotion();
                }
                else
                {
                    PlayerManager.Instance.shipsConnected = false;
                }
            }
            if (Input.GetButtonUp("Switch"))
            {
                Time.timeScale = 1;
            }
        }
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
