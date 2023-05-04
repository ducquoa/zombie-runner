using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    NavMeshAgent navMeshAgent;
    bool isDead = false;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        //GetComponent<EnemyAI>().OnDamageTaken();   cach nay cung duoc nhung de toi uu, de cho moi script co lien biet enemy dang take damage thi dung broadcast
       
        BroadcastMessage("OnDamageTaken", damage);
        hitPoints -= damage;
        if (hitPoints <= 0)
            {
                Die();
                
            }
        
    }

    

    private void Die()
    {
        if (isDead)
        {
            return;
        }
        else
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            //navMeshAgent.enabled = false;    //khong the dung day vi, co the goi de dung nhung khong truyen gia tri qua enemyAI duoc
            GetComponent<CapsuleCollider>().enabled = false; 
        }
        
    }
}
