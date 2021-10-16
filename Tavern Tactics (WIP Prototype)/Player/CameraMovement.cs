using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private PlayerControls playerControls;

    public float rotateSpeed;
    public float moveSpeed;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Read Rotate Value and execute RotateCam
        float rotateInput = playerControls.Camera.Rotate.ReadValue<float>();
        if (rotateInput != 0) 
        {
            RotateCam(rotateInput);
        }

        //Read Move Values and execute MoveCam
        float forwardInput = playerControls.Camera.Forward.ReadValue<float>();
        float rightInput = playerControls.Camera.Right.ReadValue<float>();
        if (forwardInput != 0 | rightInput != 0) 
        {
            MoveCam(forwardInput, rightInput);
        }
    }

    private void RotateCam(float rotateInput) 
    {
        //Rotates cam
        transform.Rotate(0.0f, rotateInput * rotateSpeed * Time.deltaTime, 0.0f);
    }

    private void MoveCam(float forwardInput, float rightInput) 
    {
        transform.Translate(rightInput * moveSpeed * Time.deltaTime, 0.0f, forwardInput * moveSpeed * Time.deltaTime);
    }
}
