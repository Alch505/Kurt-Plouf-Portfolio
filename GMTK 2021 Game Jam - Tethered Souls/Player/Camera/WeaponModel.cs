using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    public Weapon assignedWeapon;

    WeaponFunctions weaponFunctions;

    bool inAnim;

    private void Start()
    {
        weaponFunctions = GetComponentInParent<WeaponFunctions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponFunctions.weapon == assignedWeapon)
        {
            GetComponent<MeshRenderer>().enabled = true;
            //GetComponent<Animator>().enabled = true;
        }
        else 
        {
            inAnim = false;
            GetComponent<MeshRenderer>().enabled = false;
            //GetComponent<Animator>().enabled = false;
        }

        //Play Shooting animation
        if (weaponFunctions.weapon != null) 
        {
            if (!weaponFunctions.weapon.isAutomatic)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (!inAnim) 
                    {
                        StartCoroutine("Animation");
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    StartCoroutine("Animation");
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    GetComponent<Animator>().Play("Idle"); ;
                    StopAllCoroutines();
                }
            }
        }
    }

    IEnumerator Animation()
    {
        //GetComponent<Animator>().StopPlayback();
        if (!weaponFunctions.weapon.isAutomatic) 
        {
            inAnim = true;
        }

        GetComponent<Animator>().Play("Shoot");

        yield return new WaitForSeconds(weaponFunctions.weapon.rate);

        if (weaponFunctions.weapon.isAutomatic)
        {
            StartCoroutine("Animation");
        }
        else 
        {
            inAnim = false;
        }
    }
}
