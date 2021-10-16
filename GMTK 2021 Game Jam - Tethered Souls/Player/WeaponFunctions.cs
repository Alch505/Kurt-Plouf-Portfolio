using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFunctions : MonoBehaviour
{
    public Weapon weapon;

    public int curAmmo;

    public bool canShoot = true;

    public GameObject bulletHole;

    public float aimAssist;

    public GameObject rocket;

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.isDead) 
        {
            weapon = null;
        }

        //Check for if there's a weapon
        if (weapon != null && !GameManager.instance.isPaused) 
        {
            if (!weapon.isAutomatic)
            {
                if (Input.GetButtonDown("Fire1") && canShoot)
                {
                    switch (weapon.weaponName) 
                    {
                        case "Pistol":
                            PistolFire();
                            break;
                        case "RPG":
                            RPGFire();
                            break;
                    }
                }
            }
            else 
            {
                canShoot = true;

                if (Input.GetButtonDown("Fire1"))
                {
                    switch (weapon.weaponName) 
                    {
                        case "AssaultRifle":
                            StartCoroutine("ARFire");
                            break;
                    }
                }
                else if (Input.GetButtonUp("Fire1")) 
                {
                    StopAllCoroutines();
                }
            }

            if (curAmmo <= 0) 
            {
                curAmmo = 0;
                weapon = null;
            }
        }
    }

    #region Pistol
    void PistolFire() 
    {
        if (curAmmo > 0) 
        {
            canShoot = false;
            curAmmo -= 1;

            RaycastHit hit;
            if (Physics.SphereCast(transform.position, aimAssist, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
            {
                hit.transform.GetComponent<Enemy>().curHealth -= weapon.damage;
            }
            else if (Physics.SphereCast(transform.position, aimAssist, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) 
            {
                Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            }

            StartCoroutine("PistolCooldown");
        }
    }

    public IEnumerator PistolCooldown() 
    {
        yield return new WaitForSeconds(weapon.rate);
        canShoot = true;
    }

    #endregion

    #region AR
    IEnumerator ARFire() 
    {
        if (curAmmo > 0)
        {
            canShoot = false;
            curAmmo -= 1;

            RaycastHit hit;
            if (Physics.SphereCast(transform.position, aimAssist, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
            {
                hit.transform.GetComponent<Enemy>().curHealth -= weapon.damage;
            }
            else if (Physics.SphereCast(transform.position, aimAssist, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            }
            
            yield return new WaitForSeconds(weapon.rate);

            StartCoroutine("ARFire");
        }
    }
    #endregion

    #region RPG
    void RPGFire()
    {
        if (curAmmo > 0)
        {
            canShoot = false;
            curAmmo -= 1;

            Instantiate(rocket, transform.position, transform.rotation);

            StartCoroutine("RPGCooldown");
        }
    }

    public IEnumerator RPGCooldown()
    {
        yield return new WaitForSeconds(weapon.rate);
        canShoot = true;
    }

    #endregion
}
