using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    [SerializeField] private WallRun wallRun;

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void SetSensitivity(float set) 
    {
        mouseSensitivity = set;
    }
}
