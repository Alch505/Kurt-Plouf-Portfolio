using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityUI : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = $"Mouse Sensitivity: {GameManager.instance.player.GetComponentInChildren<FPSCam>().mouseSensitivity}";
    }
}
