using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPickup : MonoBehaviour
{
    public int healAmount;

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, 2.5f, LayerMask.GetMask("Player"))) 
        {
            PlayerManager.instance.curHealth += healAmount;

            Destroy(this.gameObject);
        }
    }
}
