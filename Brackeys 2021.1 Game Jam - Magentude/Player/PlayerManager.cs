using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region
    private static PlayerManager _instance;

    public static PlayerManager Instance { get { return _instance; } }


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
    }
    #endregion

    public int health;

    public bool gameOver;

    public GameObject LShip;
    public GameObject LShipObj;
    public GameObject LShipChar;

    public GameObject RShip;
    public GameObject RShipObj;
    public GameObject RShipChar;

    public bool connecting;
    public bool shipsConnected;

    public bool invincible;

    public bool dead;

    public string powerUp = "normal";
    public float powerLength;

    //bool areConnected;

    public void Update()
    {
        connecting = LShip.GetComponent<Movement>().comingTogether;

        if (health <= 0 && !dead) 
        {
            dead = true;

            GameManager.Instance.StartGameOver();
        }

        //if (invincible) 
        //{
        //    StartCoroutine("Flickering");
        //}
    }

    public void TakeDamage() 
    {
        if (!invincible && powerUp != "Shield(Clone)") 
        {
            AudioDirector.Instance.PlayDamage();
            invincible = true;

            StartCoroutine("FreezeFrame");
            StartCoroutine("Invincible");

            health -= 1;
        }
    }

    IEnumerator Invincible() 
    {
        StartCoroutine("Flickering");
        yield return new WaitForSeconds(1.5f);
        
        invincible = false;
    }

    IEnumerator Flickering() 
    {
        LShipObj.SetActive(false);
        LShipChar.SetActive(false);

        RShipObj.SetActive(false);
        RShipChar.SetActive(false);

        yield return new WaitForSeconds(0.05f);

        LShipObj.SetActive(true);
        LShipChar.SetActive(true);

        RShipObj.SetActive(true);
        RShipChar.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        if (invincible) 
        {
            StartCoroutine("Flickering");
        }
        else
        {
            LShipObj.SetActive(true);
            LShipChar.SetActive(true);

            RShipObj.SetActive(true);
            RShipChar.SetActive(true);
            StopCoroutine("Flickering");
        }
    }

    IEnumerator FreezeFrame()
    {
        Time.timeScale = 0;

        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 1f;
    }
}
