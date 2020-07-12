using UnityEngine;

//this is the foundation for all the item scripts, Equipment, consumeables etc.
//scripts will derive from this template.

//this is how we will navigate in unity to create new items. 
//up the top of create/inventory/item
[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject 
    //here we are saying items are scriptable object, makes it easier?
{

    //item details will be called upon for the stats/ui
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    //marking as virtual we can derive to this script but,
    //we can make different items do different Use methods.
    public virtual void Use()
    {
        //here we can use the item
        //here is how we get Consumeables to apply direct affect
        Debug.Log("Using " + name);
    }

    //We have multiple items we want to remove, so this is the best place to have it
    //i believe for more equipment.
    public void RemoveFromInventory()
    {
        //calls Inventory.cs Remove method
        Inventory.instance.Remove(this);
    }

}
