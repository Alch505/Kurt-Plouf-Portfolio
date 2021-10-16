using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public int timer;

    private void Start()
    {
        StartCoroutine("DestroyThis");
    }

    IEnumerator DestroyThis() 
    {
        yield return new WaitForSeconds(timer);

        Destroy(this.gameObject);
    }
}
