using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    PlayerControls controls;
    public CharacterController controller;

    public GameObject otherShip;

    public bool isLeftShip;

    public bool comingTogether;
    public bool isConnected;
    public bool justHit;

    public float moveSpeed;

    float horizontal;
    float vertical;

    private Shake shake;

    private void Awake()
    {
        controls = new PlayerControls();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Handles TakeDamage()
        if (hit.gameObject.CompareTag("Damager"))
        {
            if (!hit.gameObject.GetComponent<Bullet>().actuallyBullet) 
            {
                //StartCoroutine("FreezeFrame");
                //shake.EnemyShake();

                Destroy(hit.collider.gameObject);
                PlayerManager.Instance.TakeDamage();
            }
        }

        if (!comingTogether && hit.gameObject.CompareTag("Enemy"))
        {
            //if (!PlayerManager.Instance.invincible && PlayerManager.Instance.powerUp != "Shield(Clone)")
            //{
            //    StartCoroutine("FreezeFrame");
            //    shake.EnemyShake();
            //}

            PlayerManager.Instance.TakeDamage();
        }

        //Handles when they collide with things as they come back
        if (comingTogether) 
        {
            if (hit.gameObject.tag == "Player")
            {
                if (GameManager.Instance.comboCount > 0) 
                {
                    //Adds to score by multiplying the currently banked points by the combo. Also multiplies by 2 is the power up is active 
                    if (PlayerManager.Instance.powerUp == "x2(Clone)")
                    {
                        GameManager.Instance.curScore += GameManager.Instance.comboScore * GameManager.Instance.comboCount * 2;
                    }
                    else 
                    {
                        GameManager.Instance.curScore += GameManager.Instance.comboScore * GameManager.Instance.comboCount;
                    }

                    //Resets combo
                    GameManager.Instance.comboCount = 0;
                    GameManager.Instance.comboScore = 0;
                }

                //Plays sound
                AudioDirector.Instance.PlayConnect();

                //Stops ships movement towards eachother and sets state as connected
                comingTogether = false;
                isConnected = true;
                PlayerManager.Instance.shipsConnected = true;
            }

            //Kills enemy on collision
            if (hit.gameObject.tag == "Enemy") 
            {
                hit.gameObject.GetComponent<Enemy>().Die();
                StartCoroutine("FreezeFrame");
            }
        }
    }

    //Freezes time briefly to give impact to hits
    IEnumerator FreezeFrame() 
    {
        Time.timeScale = 0;
        
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 1f;
    }

    public void Update()
    {
        //If ships get pushed from eachother by external means, this will disconnect them.
        if (Vector3.Distance(transform.position, otherShip.transform.position) > 2f) 
        {
            //AudioDirector.Instance.PlayDisconnect();
            isConnected = false;
        }

        //Cause MoveTowardsTarget Method
        if (comingTogether) 
        {
            MoveTowardsTarget(otherShip.transform.position);
        }

        //Check for Switch input
        if (!GameManager.Instance.gameOver && !GameManager.Instance.paused) 
        {
            if (Input.GetButtonUp("Switch"))
            {
                if (!isConnected)
                {
                    //AudioDirector.Instance.PlaySpeedUp();
                    comingTogether = true;
                    shake.getMag();
                }
                else
                {
                    AudioDirector.Instance.PlayDisconnect();
                    isConnected = false;
                }
            }
        }

    }
    public void FixedUpdate()
    {
        //Split Movement
        if (!comingTogether && !isConnected) 
        {
            justHit = false;
            if (isLeftShip)
            {
                //horizontal = Input.GetAxisRaw("LSHorizontal");
                //vertical = Input.GetAxisRaw("LSVertical");
                horizontal = controls.Gameplay.LSHorizontal.ReadValue<float>();
                vertical = controls.Gameplay.LSVertical.ReadValue<float>();
            }
            else
            {
                //horizontal = Input.GetAxisRaw("RSHorizontal");
                //vertical = Input.GetAxisRaw("RSVertical");
                horizontal = controls.Gameplay.RSHorizontal.ReadValue<float>();
                vertical = controls.Gameplay.RSVertical.ReadValue<float>();
            }
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                controller.Move(direction * moveSpeed * Time.deltaTime);
            }
        }

        //Connected Movement
        if (isConnected) 
        {
            CollisionFX();
            //horizontal = Input.GetAxisRaw("LSHorizontal");
            //vertical = Input.GetAxisRaw("LSVertical");
            horizontal = controls.Gameplay.LSHorizontal.ReadValue<float>();
            vertical = controls.Gameplay.LSVertical.ReadValue<float>();

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                controller.Move(direction * (moveSpeed * 0.80f) * Time.deltaTime);
            }
        }

        //Coming together
        if (comingTogether && !isConnected) 
        {
            justHit = true;
            if (isLeftShip)
            {
                //horizontal = Input.GetAxisRaw("LSHorizontal");
                //vertical = Input.GetAxisRaw("LSVertical");
                horizontal = controls.Gameplay.LSHorizontal.ReadValue<float>();
                vertical = controls.Gameplay.LSVertical.ReadValue<float>();
            }
            else
            {
                //horizontal = Input.GetAxisRaw("RSHorizontal");
                //vertical = Input.GetAxisRaw("RSVertical");
                horizontal = controls.Gameplay.RSHorizontal.ReadValue<float>();
                vertical = controls.Gameplay.RSVertical.ReadValue<float>();
            }

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                controller.Move(direction * (moveSpeed / 2) * Time.deltaTime);
            }
        }
    }

    void MoveTowardsTarget(Vector3 target) 
    {
        var offset = target - transform.position;

        if (offset.magnitude > .1f) 
        {
            offset = offset.normalized * (moveSpeed * 2.5f);

            controller.Move(offset * Time.deltaTime);
        }
    }

    public void HitByBullet()
    {
        StartCoroutine("FreezeFrame");
        //Destroy(hit.collider.gameObject);
        //if (comingTogether)
        //{
        //    PlayerManager.Instance.connecting = false;
        //    comingTogether = false;
        //    otherShip.GetComponent<Movement>().comingTogether = false;
        //}
        PlayerManager.Instance.TakeDamage();
    }

    //Handles enabling and disabling controls 
    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }


    //Pools spark prefab over player collision
    public void CollisionFX()
    {
        if (justHit == true && isLeftShip)
        {
            GameObject CollisionEffect = ObjectPooler.SharedInstance.GetPooledObject("SparkEffect");
            if (CollisionEffect != null)
            {
                justHit = false;
                shake.CamShake();
                CollisionEffect.transform.position = (transform.position + otherShip.transform.position) / 2f;
                CollisionEffect.SetActive(true);
                StartCoroutine(RemoveAfterSeconds(0.5f, CollisionEffect));
            }
        }

    }

    public IEnumerator RemoveAfterSeconds(float seconds, GameObject CollisionEffect)
    {
        yield return new WaitForSeconds(seconds);
        CollisionEffect.SetActive(false);
    }

}
