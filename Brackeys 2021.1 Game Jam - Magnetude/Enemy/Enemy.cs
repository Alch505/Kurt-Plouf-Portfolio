using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int value;
    public float chance;

    public bool isDummy;


    public void Start()
    {
        //Difficulty Score modifiers
        if (GameManager.Instance.difficulty == 5)
        {
            value *= 2;
        }
        if (GameManager.Instance.difficulty > 5)
        {
            value *= 3;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Handles shockwave collison
        if (other.tag == "Shockwave")
        {
            Die();
        }
    }

    public void Update()
    {
        //Move Enemy to make them level with playing field
        if (transform.position.y > 0) 
        {
            transform.Translate(0f, -20f * Time.deltaTime, 0f);
        }
        if (transform.position.y < 0)
        {
            transform.Translate(0f, 20f * Time.deltaTime, 0f);
        }
        //else 
        //{
        //    transform.position.y *= 0;
        //}
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!PlayerManager.Instance.connecting) 
        {
            if (hit.gameObject.tag == "Player")
            {
                PlayerManager.Instance.TakeDamage();
            }
        }
    }

    public void Die() 
    {
        //Handle comboCount and comboScore
        GameManager.Instance.comboCount += 1;

        if (!isDummy) 
        {
            GameManager.Instance.comboScore += value;

            RoundManager.Instance.enemiesOut -= 1;
        }

        //Play sounds
        AudioDirector.Instance.PlayExplosion();
        AudioDirector.Instance.PlayHit(GameManager.Instance.comboCount - 1);

        //... C'mon...
        Destroy(this.gameObject);

        //Handle Spark effect
        GameObject EnemySparks = ObjectPooler.SharedInstance.GetPooledObject("EnemySparks");
        EnemySparks.transform.position = (transform.position);
        EnemySparks.SetActive(true);
    }
}
