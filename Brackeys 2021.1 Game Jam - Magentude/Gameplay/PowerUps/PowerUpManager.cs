using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    #region
    private static PowerUpManager _instance;

    public static PowerUpManager Instance { get { return _instance; } }


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

    public GameObject[] spawns;
    public GameObject[] powerUps;

    public GameObject display;

    public bool powerUpOut;
    public bool powerUpOn;

    private int powerUpChosen;
    private int lastPowerUpType;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnPowerUp");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Instance.powerUp != "normal") 
        {
            //Gives the power up an active timer when collected as long as it's not a shockwave
            if (PlayerManager.Instance.powerUp != "Battery(Clone)")
            {
                powerUpOn = true;
                StartCoroutine("PowerUpOnTimer");
            }
            else if (PlayerManager.Instance.powerUp == "Battery(Clone)")
            {
                powerUpOn = true;
            }
        }

        //Prevents manager from spawning the same power up repeatedly
        if (powerUpChosen == lastPowerUpType) 
        {
            powerUpChosen = Random.Range(0, powerUps.Length);
        }
    }

    IEnumerator SpawnPowerUp() 
    {
        //Detects if a power up is out or being used to prevent spawning a power up
        if (!powerUpOn || !powerUpOut) 
        {
            //Determines time until next power up, spawn point, and which power up type
            float timeTilNext = (float)Random.Range(20, 40);
            int spawnChosen = Random.Range(0, spawns.Length);
            powerUpChosen = Random.Range(0, powerUps.Length);

            yield return new WaitForSeconds(timeTilNext);

            //Spawns power up and sets which one to avoid spawning next
            Instantiate(powerUps[powerUpChosen], spawns[spawnChosen].transform);
            lastPowerUpType = powerUpChosen;

            powerUpOut = true;
        }
        else 
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine("SpawnPowerUp");
        }
    }

    IEnumerator PowerUpOnTimer() 
    {
        //ADD TIME ACTIVE VARIABLE FOR POWER UPS TO REPLACE THIS
        //if (PlayerManager.Instance.powerLength != 0)
        //{

        //}
        //else 
        //{
        //    yield return new WaitForSeconds(3f);
        //}

        //Wait for length of power up
        yield return new WaitForSeconds(PlayerManager.Instance.powerLength);

        //Start flickering
        display.GetComponent<PowerUpDisplay>().StartCoroutine("Flickering");

        //Always have a 2 second warning for power end
        yield return new WaitForSeconds(2f);

        //Stop flickering, reset player power up state, restart spawn timer, and end this coroutine
        display.GetComponent<PowerUpDisplay>().StopCoroutine("Flickering");
        PlayerManager.Instance.powerUp = "normal";
        powerUpOn = false;

        StartCoroutine("SpawnPowerUp");
        StopCoroutine("PowerUpOnTimer");
    }
}
