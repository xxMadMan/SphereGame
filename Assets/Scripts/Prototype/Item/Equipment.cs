using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Similar to item creation, here we are making a new one for all Equipment
//we are also making it accessable when we right click in the editor and follow the path,
// Create / Inventory / Equipment
[CreateAssetMenu(fileName ="New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item //we use the base class Item.cs
{

    public EquipmentSlot equipSlot;
    public GameObject equipmentObj;


    //Here we have all the modifiers for equipment and weapons (maybe we make a new area for weapons?)
    //and here is where scripts will recieve the modifier data to apply else where
    public int armourModifier;
    public int damageModifier;
    public int healthModifier;

    //here we call the base class Item.cs Use method.
    //Because we want equipment to apply we will ovveride the Use method to do another command
    public override void Use()
    {
        base.Use();
        //this is the equip method from EquipmentManager.
        //where the equipment manager will get its data for this script is as shown below
        //We want to call upon the Equip method in EquipmentManager.cs feeding it the data
        // from (this); script
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory(); //Method from our base class to remove item
    }

}

//here we will define equipment slots, so we use an enum and we want it outside out class to use in
//multiple places. here we have defined different slots so instead of writing
// public Int equipSlots, we can write, public EquipmentSlot equipslot.
public enum EquipmentSlot { Head, Weapon, Shield}
