using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public CharacterController controller;

    //public float speed = 8f;

    public bool actuallyBullet = true;

    private void Start()
    {
        //Check to make sure DestroyThis is necessary
        if (actuallyBullet) 
        {
            StartCoroutine("DestroyThis");
        }
    }

    private void Update()
    {
        //Destroys bullet during TimeStop
        if (PlayerManager.Instance.powerUp == "TimeStop(Clone)" && actuallyBullet) 
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Movement>().HitByBullet();
            Destroy(this.gameObject);
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyThis() 
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
}
