using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    void Update()
    {
        if (GameManager.instance.player.GetComponentInChildren<WeaponFunctions>().weapon != null)
        {
            GetComponent<Text>().text = "Ammo: " + GameManager.instance.player.GetComponentInChildren<WeaponFunctions>().curAmmo;
        }
        else 
        {
            GetComponent<Text>().text = "";
        }
    }
}
