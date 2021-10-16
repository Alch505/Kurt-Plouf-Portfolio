using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camAnim;

    public GameObject leftShip;
    public GameObject rightShip;

    public float mag;

    public void getMag()
    {
        mag = Vector3.Distance(leftShip.transform.position, rightShip.transform.position);
    }
    public void CamShake()
    {
        if (mag >= 2 && PlayerManager.Instance.powerUp == "Battery(Clone)")
        {
            if (GameManager.Instance.screenShake) 
            {
                camAnim.SetTrigger("shake3");
            }
            Shockwave();
        }
        if (GameManager.Instance.screenShake) 
        {
            if (mag <= 16)
            {
                camAnim.SetTrigger("shake");
            }
            else if (mag <= 32)
            {
                camAnim.SetTrigger("shake2");
            }
            else if (mag > 32)
            {
                camAnim.SetTrigger("shake3");
            }
        }
    }

    public void EnemyShake()
    {
        camAnim.SetTrigger("shake2");
    }

    public void Shockwave()
    {
        GameObject Shockwave = ObjectPooler.SharedInstance.GetPooledObject("Shockwave");
        Shockwave.transform.position = (rightShip.transform.position + leftShip.transform.position) / 2f;
        Shockwave.SetActive(true);
        StartCoroutine(RemoveAfterSeconds(2f, Shockwave));
        PlayerManager.Instance.powerUp = "normal";
        PowerUpManager.Instance.powerUpOn = false;
        PowerUpManager.Instance.StartCoroutine("SpawnPowerUp");
    }

    IEnumerator RemoveAfterSeconds(float seconds, GameObject Shockwave)
    {
        yield return new WaitForSeconds(seconds);
        Shockwave.SetActive(false);
        GameManager.Instance.curScore += GameManager.Instance.comboScore * GameManager.Instance.comboCount;
        GameManager.Instance.comboCount = 0;
        GameManager.Instance.comboScore = 0;
    }
}
