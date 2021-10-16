using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = $"Health: {PlayerManager.instance.curHealth} / {PlayerManager.instance.maxHealth}"; 
    }
}
