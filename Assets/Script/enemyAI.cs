using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float moveSpeed = 7f;
    EnemyHealth health;
    

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    Transform target;

    void Start()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    
    public void OnDamageTaken(float damage)
    {
        isProvoked = true;
    }
    
    void Update()
    {
        if (health.GetIsDead())
        {
            enabled = false;    //turn off enemy component in game object so it wont attack or doing anything, but navmesh still works
            navMeshAgent.enabled = false;
        }
        else
        {
            distanceToTarget = Vector3.Distance(target.position, transform.position);
            if (isProvoked)
            {
                EngageTarget();
            }
            else if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;

            }
        }

    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();  
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("die", false);
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.SetDestination(target.position);
        
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
        
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;  //normalized is used here to return a vector = 1, basically means we dont care about the distance, only the direction
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // y=0 de khoa' axis khong cho enemy ngang dau len xuong
        //transform.rotation = where the target is, we need to rotate at a certain speed
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed); ;

    }
    
     void OnDrawGizmosSelected()
    {

        //Display the chase radius when selected
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

    } 
}
