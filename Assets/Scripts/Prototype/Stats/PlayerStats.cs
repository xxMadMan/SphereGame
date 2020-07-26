using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we derive (this).cs from the base class CharacterStats.cs
public class PlayerStats : CharacterStats
{
    Animator anim;
    
    void Start()
    {
        //We use the EquipmentManager.cs instance method .onEquipmentChanged and subscribe it to
        //our own method from within this script OnEquipmentChanged;
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        anim = GetComponent<Animator>();
    }

    //this is gonna take in a piece of Equipment called newItem, and oldItem
    //just as we defined it within the EquipmentManager.cs
    //----to apply this go over to Stat.cs------
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        //we check to make sure there is a new item
        if (newItem != null)
        {
            //here we simply call apply from the baseclass armour
            //then we .AddModifier from the stat class
            //feeding in Equipment newItem 's
            //Equipment's stat we made
            armour.AddModifier(newItem.armourModifier);
            //we are adding the feeded data right back into the CharacterStat.cs, Stat.cs variable
            damage.AddModifier(newItem.damageModifier);
            health.AddModifier(newItem.healthModifier);
        }

        //similar to new item when we make it an old item we remove the modifiers
        //we use newItem / oldItem from the feeding we did in OnEquipmentChange
        if (oldItem != null)
        {
            armour.RemoveModifier(oldItem.armourModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            health.RemoveModifier(oldItem.healthModifier);
        }
    }

    //here we are overriding the Die method from our base class
    public override void Die()
    {
        base.Die();
        anim.SetInteger("Condition", 3);
    }

    public void DeathReset()
    {
        //call the PlayerManager.cs instance, using the KillPlayer method
        PlayerManager.instance.KillPlayer();
    }
}
