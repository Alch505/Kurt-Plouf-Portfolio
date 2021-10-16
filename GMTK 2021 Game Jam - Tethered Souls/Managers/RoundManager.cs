using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    #region Singleton
    public static RoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [Header("Main Settings")]

    public int round;

    public int enemiesPerRound;
    public int enemiesLeft;
    public int maxEnemiesOut;

    public int enemiesAlive;

    public Transform[] spawners;

    [Header("Enemy Types")]

    public int typesAllowed;
    public int totalTypes;

    public GameObject[] enemyTypes;

    public float spawnRate;

    [Header("HP Pickups")]

    public Transform[] hpSpawners;
    public GameObject HPPickup;

    void Start()
    {
        StartRound();
    }
    
    void Update()
    {
        if (enemiesLeft <= 0 && enemiesAlive <= 0) 
        {
            StartRound();
        }
    }

    void StartRound() 
    {
        round += 1;

        //Increase enemiesPerRound
        if (enemiesPerRound <= 40)
        {
            enemiesPerRound += 3;
        }
        else 
        {
            enemiesPerRound = 40;
        }

        maxEnemiesOut = enemiesPerRound / 2;

        enemiesLeft = enemiesPerRound;

        //Increase enemy types
        if (typesAllowed < totalTypes) 
        {
            typesAllowed += 1;
        }

        if (round % 3 == 0) 
        {
            int hpspawn = Random.Range(0, 4);

            Instantiate(HPPickup, hpSpawners[hpspawn].position, hpSpawners[hpspawn].rotation);
        }

        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy() 
    {
        if (enemiesAlive <= maxEnemiesOut) 
        {
            enemiesLeft -= 1;
            enemiesAlive += 1;

            int enemy = (int)Random.Range(0, typesAllowed);
            int spawn = (int)Random.Range(0, 3);

            Instantiate(enemyTypes[enemy], spawners[spawn]);
        }

        yield return new WaitForSeconds(spawnRate);

        if (enemiesLeft > 0) 
        {
            StartCoroutine("SpawnEnemy");
        }
    }
}
