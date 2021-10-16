using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //This is for the shield mesh so it knows to show at the right time
    void Update()
    {
        if (PlayerManager.Instance.powerUp == "Shield(Clone)")
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        else 
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
