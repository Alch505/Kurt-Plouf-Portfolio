using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    public CharacterController controller;

    public int shots = 1;
    public Rigidbody bullet;
    public Transform bulletSpawn;
    public float bulletSpeed = 6f;

    public int target;

    public int speed;
    public int normalSpeed;

    public bool canShoot;

    public Animator animator;

    void Start()
    {
        //Set shots based on difficulty
        if (GameManager.Instance.difficulty == 5)   //Expert
        {
            shots = 3;
        }

        if (GameManager.Instance.difficulty > 5)   //Master
        {
            shots = 5;
        }

        //Set target ship
        target = Random.Range(0, 2);

        //Difficulty modifiers
        if (GameManager.Instance.difficulty >= 5)
        {
            speed += 1;
        }

        //Set base Speed
        normalSpeed = speed;

        //Start AI
        StartCoroutine("AimAndShoot");
    }

    // Update is called once per frame
    void Update()
    {
        //Handles movement and rotation if Time stop is inactive
        if (PlayerManager.Instance.powerUp != "TimeStop(Clone)") 
        {
            if (!canShoot)
            {
                canShoot = true;
                StartCoroutine("AimAndShoot");
            }

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
        else
        {
            canShoot = false;
            speed = normalSpeed;
            StopCoroutine("AimAndShoot");
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

    IEnumerator AimAndShoot() 
    {
        //Difficulty modifiers for how long between shots
        if (GameManager.Instance.difficulty < 5) 
        {
            yield return new WaitForSeconds(4f);
        }

        if (GameManager.Instance.difficulty >= 5)
        {
            yield return new WaitForSeconds(3f);
        }

        //Lower speed
        speed /= 3;
        animator.SetBool("IsCharging", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("IsCharging", false);

        //Fire shots based on how many are allowed
        for (int i = 0; i < shots; i++) 
        {
            if (!PlayerManager.Instance.connecting && PlayerManager.Instance.powerUp != "TimeStop(Clone)")
            {
                Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bulletClone.velocity = bulletSpawn.transform.forward * bulletSpeed;
                animator.SetTrigger("Shoot");

                yield return new WaitForSeconds(0.5f);
            }
        }

        yield return new WaitForSeconds(1.5f);
        
        //Reset
        speed *= 3;
        StartCoroutine("AimAndShoot");
    }
}
