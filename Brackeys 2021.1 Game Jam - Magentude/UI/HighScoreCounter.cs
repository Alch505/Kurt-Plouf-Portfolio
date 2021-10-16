using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreCounter : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = $"Hi-Score: {GameManager.Instance.highScores[0]}";
    }
}
