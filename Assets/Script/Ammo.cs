using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;
    
    [System.Serializable] 
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    public int GetCurrentAmmo(AmmoType ammoType)       //can biet sung dang dung type nao
    {                                
        return GetAmmoSlot(ammoType).ammoAmount;    //pass ammo type into ammo slot to return ammo amount
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;      //to reduce current ammo, first find ammoslot and find in ammo type the amount to reduce
    }
    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)  //new argument int ammoAmout to be able to pass in the next line
    {
        GetAmmoSlot(ammoType).ammoAmount+=ammoAmount;      
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;    //tra ve ammoslot
            }
        }
        return null;
    }
}
