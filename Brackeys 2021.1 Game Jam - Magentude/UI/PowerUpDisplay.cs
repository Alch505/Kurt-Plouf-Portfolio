using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpDisplay : MonoBehaviour
{
    void Update()
    {
        if (PlayerManager.Instance.powerUp == "normal") 
        {
            GetComponent<Text>().text = null;
            GetComponent<Text>().enabled = true;
        }
        if (PlayerManager.Instance.powerUp == "x2(Clone)")
        {
            GetComponent<Text>().text = "x2";
        }
        if (PlayerManager.Instance.powerUp == "Shield(Clone)")
        {
            GetComponent<Text>().text = "Shield";
        }
        if (PlayerManager.Instance.powerUp == "TimeStop(Clone)")
        {
            GetComponent<Text>().text = "Time Stop";
        }
        if (PlayerManager.Instance.powerUp == "Battery(Clone)")
        {
            GetComponent<Text>().text = "Shockwave";
        }
    }

    IEnumerator Flickering()
    {
        this.GetComponent<Text>().enabled = false;

        yield return new WaitForSeconds(0.05f);

        this.GetComponent<Text>().enabled = true;

        yield return new WaitForSeconds(0.05f);

        StartCoroutine("Flickering");
    }
}
