using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    // created a static variable called instance,
    // here we can access the inventory from anywhere by going
    // Inventory.instance,
    public static Inventory instance;
    void Awake()
    {
        //also making sure that there should only be 1 inventory!!!
        if (instance != null)
        {
            Debug.LogWarning("MORE THAN ONE INSTANCE OF THE INVENTORY!");
            return;
        }
        instance = this; //setting Inventory instance, to this script
    }
    #endregion


    //here we subscribe different methods

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    //limit the amount of items you can have at once
    public int space = 20;


    //this is how the inventory is created/item slots
    public List<Item> items = new List<Item>();

    #region Inventory, Adding item data in
    //here is how we add the item from the world into our inventory

    //here we have made a bool rather than a void
    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            //here if the players Inv. has maxed, we want to ignore items in the world
            //as the inventory is now full.
            if (items.Count >= space)
            {
                //if the Inv. is full
                Debug.Log("Not enough room.");

                //here if the inventory is full we return false;
                return false;
            }
            //not full? call this
            items.Add(item);

            //when items are added this will invoke all scripts that have
            //subscribed to this method.
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        //if the inventory isnt full we return true
        return true;
    }
    #endregion

    #region Inventory, removing item data out
    //here is how we remove the item, here we can look at instantiate so the item can
    //look like its been dropped from the inventory!

    //
    public void Remove(Item item)
    {
        //set up code to instantiate the obj when removed from inventory
        items.Remove(item);

        //we want this method similar to add to always update when we edit the Inv.
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    #endregion
}