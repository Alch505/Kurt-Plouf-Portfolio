using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltMusic : MonoBehaviour
{
    public GameObject[] musicBanks;

    public void Awake()
    {
        //Checks difficulty to choose Music Bank
        if (GameManager.Instance.difficulty < 5)
        {
            musicBanks[0].SetActive(true);
        }
        else 
        {
            musicBanks[1].SetActive(true);
        }
    }
}
