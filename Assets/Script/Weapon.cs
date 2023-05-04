using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = true;

    private void OnEnable()    //tao method nay vi khi doi ve sung cu thi khong ban duoc nua 
    {
        canShoot = true;
    }
    void Update()
    {
        DisplayAmmo();
        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {
            StartCoroutine(Shoot());       //doi method cua shoot thanh startcoroutine
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()      //doi private void thanh IEnum
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;

    }

    void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            //add hit fx

            Debug.Log("I hit " + hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();  //create 'target' var of class EnemyHealth, this 'target is not 'target in enemyAI script

            if (target == null)      //prevent warning/errors when hit a target with no enemy script
            {
                return;
            }

            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }
}
