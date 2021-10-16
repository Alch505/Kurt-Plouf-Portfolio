using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GruntMovement : MonoBehaviour
{
    public float detectionRadius = 5000f;
    public float stopRadius = 3.5f;

    public float moveSpeed = 5f;

    NavMeshAgent agent;

    private GameObject thisObj;
    public Transform thisTransform;
    public Transform playerTransform;

    private void OnDrawGizmosSelected()
    {
        if (thisTransform == null)
        {
            thisTransform = transform;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawWireSphere(transform.position, stopRadius);
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameManager manager = GameManager.instance;
        thisObj = this.gameObject;

        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopRadius;
        //playerTransform = manager.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager manager = GameManager.instance;
        //BattleManager battleManager = BattleManager.instance;

        playerTransform = manager.player.transform;
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= detectionRadius)
        {
            if (distance > stopRadius)
            {
                if (!GetComponentInParent<Enemy>().isTethered)
                {
                    agent.speed = moveSpeed;
                }
                else 
                {
                    agent.speed = 0;
                }

                agent.SetDestination(playerTransform.position);

                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
