using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject rangePivot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnManager.Instance.curTurn != null)
        {
            if (TurnManager.Instance.curTurn.name != GetComponent<CharacterStats>().charName)
            {
                rangePivot.SetActive(false);
            }
            else
            {
                rangePivot.SetActive(true);
            }
        }
        else 
        {
            rangePivot.SetActive(false);
        }
    }
}
