using UnityEngine;
using UnityEngine.UI;


//this scripts will handle all the inventory slot updating, when items get added/removed
//this will update the visuals for it.
public class InventorySlot : MonoBehaviour
{
    //reference to the icon
    public Image icon;
    //reference to when we want to remove items
    //this will remove it from the slot visually!
    public Button removeButton; // this needs to be manually added to the script!

    //this will keep track of the item in the slot
    Item item;


    public void AddItem(Item newItem)
    {
        // applying the Item data from the script Item.cs
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        //when an item is in the Inv. a little x for removing will apear
        removeButton.interactable = true;
    }

    //Making sure all item data is removed from the slot when used/consumed
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        //when there is no item we want the x to disapear!
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        //calls Inventory.cs Remove method (with what item to remove) this;
        Inventory.instance.Remove(item);
    }

    //here is how we use the items in the slot!
    public void UseItem()
    {
        if (item != null) //here we are checking if there is an item in the slot
        {
            //here we call upon Item.cs Use method
            item.Use();
        }
    }

}
