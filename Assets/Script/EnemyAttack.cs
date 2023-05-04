using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;    //before: [SerializeField] Transform target  :who is it we are attacking, "here is where the target is"
                                            //after: PlayerHealth (type): to access PlayerHealth ezier and will find target below
    [SerializeField] float damage = 40f;


    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();    //add this here after the change so we can find the target, it's ok to use FindObject in Start this case
    }

    public void OnDamageTaken()
    {
        /* Debug.Log(name + " also know I took damage");   //test BroadcastMessage  */
    }


    public void AttackHitEvent()
    {
        if (target == null) { return; }
        target.TakeDamage(damage);           //already accessed to PlayerHealth above so don't need to GetComponent anymore
        target.GetComponent<DisplayDamage>().ShowDamage();
    }


}
