using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnUI : MonoBehaviour
{
    #region
    private static PlayerTurnUI _instance;

    public static PlayerTurnUI Instance { get { return _instance; } }


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
        //DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public string menuState;

    public GameObject[] menuItems;

    // Start is called before the first frame update
    void Start()
    {
        //REMOVE WHEN TURNS ARE INTRODUCED!!!
        menuState = "MainMenu";
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if state name matches menu item list to set correct menu elements
        if (TurnManager.Instance.isPlayerTurn)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (menuItems[i].name == menuState)
                {
                    menuItems[i].SetActive(true);
                }
                else
                {
                    menuItems[i].SetActive(false);
                }
            }
        }
        else 
        {
            for (int i = 0; i < menuItems.Length; i++) 
            {
                menuItems[i].SetActive(false);
            }
        }
    }

    //Button functions----------------------------------------------------------------------------------------
    public void MoveButton() 
    {
        TurnManager.Instance.playerTurnState = "Moving";
        menuState = "Cancel";
    }

    public void MeleeButton() 
    {
        TurnManager.Instance.playerTurnState = "MeleeAttack";
        menuState = "Cancel";
    }

    public void MagicButton() 
    {
        TurnManager.Instance.playerTurnState = "MagicAttack";
        menuState = "Cancel";
    }

    public void InventoryButton() 
    {
        menuState = "Cancel";
    }

    public void CancelButton() 
    {
        TurnManager.Instance.playerTurnState = null;
        menuState = "MainMenu";
    }
}
