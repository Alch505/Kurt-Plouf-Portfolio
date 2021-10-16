using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = $"Health: {PlayerManager.Instance.health}";
    }
}
