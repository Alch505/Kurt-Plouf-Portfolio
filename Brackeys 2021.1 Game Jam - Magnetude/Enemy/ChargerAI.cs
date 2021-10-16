using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAI : MonoBehaviour
{
    public CharacterController controller;

    public int target;

    public int speed;
    public int normalSpeed;
    public float rotateSpeed;

    public bool canCharge;
    public bool isCharging;

    public Animator animator;

    void Start()
    {
        //Select which ship to target
        target = Random.Range(0, 2);

        //Difficulty modifiers
        if (GameManager.Instance.difficulty >= 5)
        {
            speed += 1;
        }
        if (GameManager.Instance.difficulty == 1)
        {
            speed -= 1;
        }

        //Set base speed
        normalSpeed = speed;

        //Begin Charging at player
        StartCoroutine("Charging");
    }

    // Update is called once per frame
    private void Update()
    {
        //Set forward direction for Move()
        Vector3 thisForward = this.transform.forward;

        //Only Move() when Time Stop is inactive
        if (PlayerManager.Instance.powerUp != "TimeStop(Clone)") 
        {
            controller.Move(thisForward * speed * Time.deltaTime);
        }

        //Set Target Ship Object
        GameObject targetShip;

        if (target == 0)
        {
            targetShip = PlayerManager.Instance.LShip;
        }
        else
        {
            targetShip = PlayerManager.Instance.RShip;
        }

        //Handles canCharge, and rotation 
        if (PlayerManager.Instance.powerUp != "TimeStop(Clone)") 
        {
            if (!canCharge) 
            {
                canCharge = true;
                StartCoroutine("Charging");
            }

            if (!isCharging)
            {
                Vector3 targetDirection = targetShip.transform.position - transform.position;

                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
            else if (GameManager.Instance.difficulty > 5) 
            {
                float chargingRotate = rotateSpeed / 3;
                Vector3 targetDirection = targetShip.transform.position - transform.position;

                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, chargingRotate * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
        else 
        {
            canCharge = false;
            speed = normalSpeed;
            StopCoroutine("Charging");
        }
    }

    IEnumerator Charging() 
    {
        //Slow down
        yield return new WaitForSeconds(4f);
        speed /= 2;

        //Charge
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Dash");
        speed *= 8;
        isCharging = true;

        //Cool off
        yield return new WaitForSeconds(1f);
        isCharging = false;
        speed /= 8;

        //Change Target
        if (target == 0)
        {
            target = 1;
        }
        else 
        {
            target = 0;
        }

        //Reset
        yield return new WaitForSeconds(1f);
        speed *= 2;
        StartCoroutine("Charging");
    }
}
