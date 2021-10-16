using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;

    public float lifeTime = 10f;

    public int damage = 10;

    Rigidbody rb;

    public bool isRocket;
    public bool enemys;

    public GameObject explosion;

    private void Start()
    {
        StartCoroutine("KillThis");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime, ForceMode.Force);

        if (!isRocket) 
        {
            if (Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("Ground")) == true)
            {
                Destroy(this.gameObject);
            }
            else if (Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("Player")) == true)
            {
                Destroy(this.gameObject);

                PlayerManager.instance.TakeDamage(damage);
            }
        }
        else 
        {
            if (Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("Ground")) == true)
            {
                BlowUp();
            }

            if (enemys)
            {
                if (Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("Player")) == true)
                {
                    BlowUp();
                }
            }
            else 
            {
                if (Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("Enemy")) == true)
                {
                    BlowUp();
                }
            }
        }
    }

    IEnumerator KillThis() 
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    void BlowUp() 
    {
        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }
}
