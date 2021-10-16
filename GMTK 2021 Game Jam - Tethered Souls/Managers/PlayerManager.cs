using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public int curHealth;
    public int maxHealth;

    public bool isDead;

    public bool invincible;
    public float IFrames = 1;

    private void Start()
    {
        curHealth = maxHealth;
    }

    private void Update()
    {
        if (curHealth > maxHealth) 
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0) 
        {
            Die();
            curHealth = 0;
        }
    }

    public void TakeDamage(int damage) 
    {
        if (!invincible) 
        {
            curHealth -= damage;
            invincible = true;

            StartCoroutine("Invincibility");
        }
    }

    IEnumerator Invincibility() 
    {
        yield return new WaitForSeconds(IFrames);
        invincible = false;
    }

    void Die() 
    {
        isDead = true;

        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
    }
}
