using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    #region
    private static RoundManager _instance;

    public static RoundManager Instance { get { return _instance; } }


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

    public int round;

    public int enemiesPerRound;
    public int maxOnScreen;
    public int enemiesLeft;
    public int enemiesOut;

    public Transform[] spawners;
    public float spawnRateMod;
    public bool spawnedEnemy;

    public GameObject[] enemyTypes;
    public int typesAllowed = 1;
    public GameObject[] enemyBank;

    public GameObject grunt;

    void Start()
    {
        //Since Master increases typesAllowed every round starting at 0 ensures the first round is only Grunts
        if (GameManager.Instance.difficulty > 5) 
        {
            typesAllowed = 0;
        }
    }

    void Update()
    {
        //Begins end of round
        if (enemiesLeft <= 0 && enemiesOut <= 0) 
        {
            StartCoroutine("RoundDelay");
        }

        //Handles spawning while the round still has enemies left
        if (enemiesLeft > 0 && enemiesOut < maxOnScreen && PlayerManager.Instance.powerUp != "TimeStop(Clone)") 
        {
            StartCoroutine("SpawnDelay");
        }
    }

    IEnumerator RoundDelay() 
    {
        yield return new WaitForSeconds(3.0f);
        StartNewRound();
        StopCoroutine("RoundDelay");
    }

    IEnumerator SpawnDelay() 
    {
        //Set Spawn
        int spawnPoint = Random.Range(0, spawners.Length);
        
        //Wait based on round count (lowers every 10 rounds)
        yield return new WaitForSeconds(1.0f * spawnRateMod);

        //Basic Spawner
        int randomEnemy = Random.Range(0, 100);

        int gruntChance = 100 - ((typesAllowed * 20) - 20);

        spawnedEnemy = false;

        //Grunt
        if (randomEnemy >= 0 && randomEnemy <= gruntChance) 
        {
            Instantiate(enemyTypes[0], spawners[spawnPoint].position, grunt.transform.rotation);
            spawnedEnemy = true;
        }
        //Charger
        if (typesAllowed >= 2) 
        {
            if (randomEnemy > 80 && randomEnemy <= 100)
            {
                Instantiate(enemyTypes[1], spawners[spawnPoint].position, spawners[spawnPoint].rotation);
                spawnedEnemy = true;
            }
        }
        //Shooter
        if (typesAllowed >= 3)
        {
            if (randomEnemy > 60 && randomEnemy <= 80)
            {
                Instantiate(enemyTypes[2], spawners[spawnPoint].position, spawners[spawnPoint].rotation);
                spawnedEnemy = true;
            }
        }
        //Wall
        if (typesAllowed >= 4)
        {
            if (randomEnemy > 40 && randomEnemy <= 60)
            {
                Instantiate(enemyTypes[3], spawners[spawnPoint].position, spawners[spawnPoint].rotation);
                spawnedEnemy = true;
            }
        }

        //Checks to make sure an enemy actually spawned to prevent soft locking, and warn if nothing spawned
        if (spawnedEnemy) 
        {
            enemiesLeft -= 1;
            enemiesOut += 1;
        }
        else 
        {
            Debug.LogWarning("Skipped an enemy spawn!");
        }

        StopCoroutine("SpawnDelay");
    }

    void StartNewRound() 
    {
        //Disables enemy spark particles on round change
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("EnemySparks");

        foreach (GameObject go in gameObjectArray)
        {
            go.SetActive(false);
        }

        //Increase round count
        round += 1;

        //Increase enemy types allowed based on round count
        if (GameManager.Instance.difficulty <= 5)
        {
            if ((round % 3) == 0 && typesAllowed < enemyTypes.Length)
            {
                typesAllowed += 1;
                //PlayerManager.Instance.health += 1;
            }
        }
        else 
        {
            typesAllowed += 1;
        }

        //Give player health point every 5 rounds
        if ((round % 5) == 0) 
        {
            PlayerManager.Instance.health += 1;
        }

        //Calculate enemiesPerRound and maxOnScreen
        enemiesPerRound = 5 + ((round * GameManager.Instance.difficulty));
        maxOnScreen = 3 + (round * GameManager.Instance.difficulty / 2);

        //Max values for enemiesPerRound and maxOnScreen
        if (enemiesPerRound > 100) 
        {
            enemiesPerRound = 100;
        }
        if (maxOnScreen > 25) 
        {
            maxOnScreen = 25;
        }

        //Decrease spawnRateMod over time
        if (round >= 10 && round < 20) 
        {
            spawnRateMod = 0.8f;
        }
        else if(round >= 20 && round < 30)
        {
            spawnRateMod = 0.6f;
        }
        else if (round >= 30 && round < 40)
        {
            spawnRateMod = 0.4f;
        }

        //CreateEnemyBank();

        //Set enemiesLeft
        enemiesLeft = enemiesPerRound;
        return;
    }

}
