using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy", order = 1)]
public class Enemy : MonoBehaviour
{
    public Weapon weapon;

    public int curHealth;
    public int maxHealth;

    public bool isTethered;

    LineRenderer lineRenderer;

    [System.Obsolete]
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.1f, 0.1f);
    }

    public void Update()
    {
        if (isTethered)
        {
            lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, GameManager.instance.player.transform.position);
        }
        else 
        {
            lineRenderer.enabled = false;
        }

        if (curHealth <= 0) 
        {
            curHealth = 0;

            RoundManager.instance.enemiesAlive -= 1;

            if (isTethered) 
            {
                GameManager.instance.player.GetComponentInChildren<WeaponFunctions>().weapon = null;
            }

            //Find a way to bank!!!
            Destroy(this.gameObject);
        }

        if (GameManager.instance.player.GetComponentInChildren<WeaponFunctions>().weapon == null) 
        {
            isTethered = false;
        }
    }
}
