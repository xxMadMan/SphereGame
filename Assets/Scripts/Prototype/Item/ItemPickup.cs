using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//derives from the script Interactable.cs
public class ItemPickup : Interactable
{

    //here we will access all the values of the 'item'. values found in Item.cs
    public Item item;

    // overrides code from interactable script for when items are picked up
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }


    //how the player picks up items
    void PickUp()
    {
        //here we have a bool for when items are picked up they are immediately destroyed
        //we are also accessing the Add void from Inventory.cs refereing to
        //Inventory instance;
        bool wasPickedUp = Inventory.instance.Add(item);
        //here we have a bool was picked up which = the result of the Add method from Inventory.cs


        //when the item gets picked up, changes the bool, destroys item in scene.
        //when the bool becomes true the item will destroy from the world
        //simulating the player picking it up
        if (wasPickedUp)
        Destroy(gameObject);

    }

}
