using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    //this here will refer to the Inv. Slot Parent which is the UI panel or inventoryUI;
    public Transform itemsParent;
    //here we call the inventoryUI for enabling/disabling the display
    public GameObject inventoryUI;
    
    //to make sure the code is optimize 
    Inventory inventory;

    //here we create an array of Inv. Slots
    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        //here we call upon the Inventory.cs script for the on item changed call back;
        //subcribing to the even so we can update our ui method in this script
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        //here we are checking all the array components for InventorySlot.cs
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        // when we press the inventory button "Tab" we want to activate the Inv. UI
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

    }

    void UpdateUI()
    {
        //here we are looping through all of the slots 
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                //if we have items to add we are calling
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                //else we dont have any more items to add we want to call 
                //ClearSlot on that slot!
                slots[i].ClearSlot();
            }
        }
    }
}
