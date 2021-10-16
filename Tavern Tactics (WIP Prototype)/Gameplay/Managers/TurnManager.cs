using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    #region Singleton
    private static TurnManager _instance;

    public static TurnManager Instance { get { return _instance; } }


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

    public CharacterStats[] characters;
    public CharacterStats curTurn;
    public int turnNum;

    public CharacterStats[] turnOrder;

    public bool isPlayerTurn;
    public string playerTurnState;

    void Start()
    {
        SortTurnOrder();
        turnNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curTurn = turnOrder[turnNum];

        if (curTurn != null) 
        {
            if (curTurn.charName == "Player")
            {
                isPlayerTurn = true;
            }
            else
            {
                isPlayerTurn = false;
            }
        }

        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }

    public void SortTurnOrder() 
    {
        if (turnOrder == null) 
        {
            turnOrder = characters;
        }

        turnOrder = turnOrder.OrderByDescending(turnOrder => turnOrder.speed).ToArray();
    }

    public void NextTurn() 
    {
        //Checks if currently on last turn to make sure the order loops
        if (turnNum == turnOrder.Length)
        {
            turnNum = 0;
        }
        else 
        {
            turnNum += 1;
        }
    }

    private void PlayerTurn() 
    {
        
    }

    private void EnemyTurn() 
    {

    }
}