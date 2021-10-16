using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntAI : MonoBehaviour
{
    public CharacterController controller;
    
    public int target;

    public int speed;

    void Awake()
    {
        //Set target
        target = Random.Range(0, 2);

        //Difficulty Modifiers
        if (GameManager.Instance.difficulty >= 5) 
        {
            speed += 2;
        }
        if (GameManager.Instance.difficulty == 1)
        {
            speed -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Handle moving towards target if Time Stop is inactive
        if (PlayerManager.Instance.powerUp != "TimeStop(Clone)") 
        {
            if (target == 0)
            {
                MoveTowardsTarget(PlayerManager.Instance.LShip.transform.position);
                this.transform.LookAt(PlayerManager.Instance.LShip.transform);
            }
            else
            {
                MoveTowardsTarget(PlayerManager.Instance.RShip.transform.position);
                this.transform.LookAt(PlayerManager.Instance.RShip.transform);
            }
        }
    }

    void MoveTowardsTarget(Vector3 target) 
    {
        var offset = target - transform.position;

        if (offset.magnitude > .1f)
        {
            offset = offset.normalized * speed;

            controller.Move(offset * Time.deltaTime);
        }
    }
}
