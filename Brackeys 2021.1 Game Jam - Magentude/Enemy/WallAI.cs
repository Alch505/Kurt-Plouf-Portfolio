using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAI : MonoBehaviour
{
    public CharacterController controller;

    public int target;

    public int speed;

    void Awake()
    {
        //Set target ship
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
    }

    // Update is called once per frame
    void Update()
    {
        //Handles movement and rotation when Time Stop is inactive
        if (PlayerManager.Instance.powerUp != "TimeStop(Clone)") 
        {
            if (target == 0)
            {
                MoveTowardsTarget(PlayerManager.Instance.LShip.transform.position);
                if (!PlayerManager.Instance.connecting) 
                {
                    this.transform.LookAt(PlayerManager.Instance.LShip.transform);
                }
            }
            else
            {
                MoveTowardsTarget(PlayerManager.Instance.RShip.transform.position);
                if (!PlayerManager.Instance.connecting)
                {
                    this.transform.LookAt(PlayerManager.Instance.RShip.transform);
                }
            }
        }
    }

    void MoveTowardsTarget(Vector3 target)
    {
        var offset = target - transform.position;

        //Check distance to affect move speed
        if (offset.magnitude > 6f && offset.magnitude > .1f)
        {
            offset = offset.normalized * speed;

            controller.Move(offset * Time.deltaTime);
        }
        if (offset.magnitude <= 6f && offset.magnitude > .1f)
        {
            offset = offset.normalized * (speed / 2);

            controller.Move(offset * Time.deltaTime);
        }
    }
}
