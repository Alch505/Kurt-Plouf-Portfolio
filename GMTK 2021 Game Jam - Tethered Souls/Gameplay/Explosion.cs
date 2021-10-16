using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius;

    public bool isEnemys;

    public int damage;

    private void Start()
    {
        StartCoroutine("DestroyThis");

        //Check if this should damage the player or enemies
        if (isEnemys)
        {
            if (Physics.CheckSphere(transform.position, radius, LayerMask.GetMask("Player")))
            {
                //Use distance from center of explosion to calculate damage
                if ((int)Vector3.Distance(GameManager.instance.player.transform.position, transform.position) > 0)
                {
                    int tempDamage = damage / (int)Vector3.Distance(GameManager.instance.player.transform.position, transform.position);

                    PlayerManager.instance.TakeDamage(tempDamage);
                }
                else 
                {
                    PlayerManager.instance.TakeDamage(damage);
                }
            }
        }
        else
        {
            //Detect all enemies with explosion radius
            Collider[] cols = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));

            for (int i = 0; i < cols.Length; i++) 
            {
                //Use distance from center of explosion to calculate damage
                if ((int)Vector3.Distance(cols[i].transform.position, transform.position) > 0) 
                {
                    int tempDamage = damage / (int)Vector3.Distance(cols[i].transform.position, transform.position);

                    cols[i].transform.GetComponent<Enemy>().curHealth -= tempDamage;
                }
                else 
                {
                    cols[i].transform.GetComponent<Enemy>().curHealth -= damage;
                }
            }
        }
    }

    IEnumerator DestroyThis() 
    {
        yield return new WaitForSeconds(0.1f);

        Destroy(this.gameObject);
    }
}
