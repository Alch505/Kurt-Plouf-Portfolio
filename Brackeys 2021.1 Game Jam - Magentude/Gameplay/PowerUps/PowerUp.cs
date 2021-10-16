using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isTesting;

    //All power ups will have a 2 second warning period
    public float powerLength;

    public void Awake()
    {
        //Starts timer for despawn as long as the Debug function IsTesting is disabled
        if (!isTesting) 
        {
            StartCoroutine("Timer");
        }
    }

    //Detect player collision and perform audio, gameplay, and destroy functions
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioDirector.Instance.PlayPowerUpSound(0);
            PlayerManager.Instance.powerUp = this.name;
            PlayerManager.Instance.powerLength = powerLength;
            PowerUpManager.Instance.powerUpOut = false;
            Destroy(this.gameObject);
        }
    }

    //Timer for the object existing
    IEnumerator Timer() 
    {
        yield return new WaitForSeconds(7f);
        StartCoroutine("Flickering");
        yield return new WaitForSeconds(3f);
        PowerUpManager.Instance.StartCoroutine("SpawnPowerUp");
        PowerUpManager.Instance.powerUpOut = false;
        Destroy(this.gameObject);
    }

    //Flickers the mesh to show the player the power up is about to disappear
    IEnumerator Flickering() 
    {
        this.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(0.05f);

        this.GetComponent<MeshRenderer>().enabled = true;

        yield return new WaitForSeconds(0.05f);

        StartCoroutine("Flickering");
    }
}
