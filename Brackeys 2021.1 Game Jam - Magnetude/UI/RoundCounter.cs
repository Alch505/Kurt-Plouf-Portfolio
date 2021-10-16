using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCounter : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = $"Round:\n{RoundManager.Instance.round}";
    }
}
