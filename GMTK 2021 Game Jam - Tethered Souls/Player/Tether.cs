using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether : MonoBehaviour
{
    public MovementRB playerMovement;
    public WeaponFunctions weaponFunctions;

    public float range;

    public GameObject tetheredEnemy;

    public GameObject grappleObject;

    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && weaponFunctions.weapon == null) 
        {
            if (Physics.SphereCast(transform.position, 0.5f, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask.GetMask("Enemy"))) 
            {
                StartCoroutine("GetWeapon");
            }
        }

        if (Input.GetButtonDown("Fire2")) 
        {
            weaponFunctions.weapon = null;

            if (Physics.SphereCast(transform.position, 0.1f, transform.TransformDirection(Vector3.forward), out hit, range, playerMovement.groundMask)) 
            {
                GameObject target = Instantiate(grappleObject, hit.point, Quaternion.identity);

                playerMovement.grapplePoint = target.transform;
            }
        }
    }

    IEnumerator GetWeapon() 
    {
        yield return new WaitForSeconds(0.1f);

        hit.transform.gameObject.GetComponent<Enemy>().isTethered = true;
        weaponFunctions.weapon = hit.transform.GetComponent<Enemy>().weapon;
        weaponFunctions.canShoot = true;
        weaponFunctions.curAmmo = weaponFunctions.weapon.maxAmmo;
    }
}
