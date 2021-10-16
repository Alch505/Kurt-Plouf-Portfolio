using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntShoot : MonoBehaviour
{
    public Transform shotPoint;

    public GameObject bullet;

    public float shotTimer = 1f;

    bool canShoot;
    bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask.GetMask("Player")))
        {
            canShoot = true;
        }
        else 
        {
            canShoot = false;
        }

        if (canShoot) 
        {
            if (!isShooting) 
            {
                StartCoroutine("Shoot");
            }
        }

        transform.LookAt(GameManager.instance.player.transform);
    }

    IEnumerator Shoot() 
    {
        if (!GetComponentInParent<Enemy>().isTethered) 
        {
            isShooting = true;
            Instantiate(bullet, shotPoint.position, shotPoint.rotation);

            yield return new WaitForSeconds(shotTimer);
            isShooting = false;
        }
    }
}
