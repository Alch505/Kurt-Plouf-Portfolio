using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floatermovement : MonoBehaviour
{
    public float mainSpeed;
    public float aimSpeed;

    public float rotateSpeed;

    public float stoppingDistance;

    bool isAiming;

    public GameObject bullet;

    public Transform bulletPoint;

    bool inAttack;

    public float fireRate = 0.3f;
    public float cooldown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (inAttack == false && !GetComponent<Enemy>().isTethered)
        {
            StartCoroutine("Attack");
        }
        else if (GetComponent<Enemy>().isTethered) 
        {
            StopCoroutine("Attack");
            inAttack = false;
        }

        GameObject player = GameManager.instance.player;
        Vector3 thisForward = this.transform.forward;

        if (Vector3.Distance(player.transform.position, transform.position) > stoppingDistance && !GetComponent<Enemy>().isTethered) 
        {
            if (!isAiming)
            {
                GetComponent<CharacterController>().Move(thisForward * mainSpeed * Time.deltaTime);
            }
            else if (isAiming)
            {
                GetComponent<CharacterController>().Move(thisForward * aimSpeed * Time.deltaTime);
            }
            //else if (GetComponent<Enemy>().isTethered) 
            //{

            //}
        }

        Vector3 targetDirection = player.transform.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    IEnumerator Attack() 
    {
        inAttack = true;

        yield return new WaitForSeconds(cooldown);
        
        isAiming = true;

        yield return new WaitForSeconds(1);

        Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        yield return new WaitForSeconds(fireRate);
        Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        yield return new WaitForSeconds(fireRate);
        Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        yield return new WaitForSeconds(fireRate);
        Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        yield return new WaitForSeconds(fireRate);
        Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        yield return new WaitForSeconds(fireRate);

        yield return new WaitForSeconds(1);

        isAiming = false;

        inAttack = false;
    }
}
