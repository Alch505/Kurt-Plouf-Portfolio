using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{
    public int arraySlot;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = $"{GameManager.Instance.highScores[arraySlot]}";
    }
}
