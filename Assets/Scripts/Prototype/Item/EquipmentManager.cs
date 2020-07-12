using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton
    //here we can call upon methods from any other scripts
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject headPos, weaponPos; // add shieldPos

    //array for equipment for each current equipment that is applied --see start method for more
    Equipment[] currentEquipment;
    //array for the same thing above but for instantiating objs
    GameObject[] currentObj;

    //here we can now have other scripts subscribe to this callback method to then invoke it in Equip
    //if (onEquipmentChanged != null) so when we equip and unequip an item other scripts will
    //have this information scripts that use this are as listed below
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    //here we reference the inventory by cache ing it
    Inventory inventory;

    void Start()
    {
        //when the gamemanager starts inventory = to Inventory.cs singleton .instance;
        //this will be used for adding the item back into the inventory from having equiped
        inventory = Inventory.instance;

        //here we set up the numbSlots = to The Enum GetNames from the enum EquipmentSlot . how ever many
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; 
        //this next line will set the array currentEquipment = to new Equipment[numberSlots];
        currentEquipment = new Equipment[numSlots];
        currentObj = new GameObject[numSlots];
    }

    //this method takes in a piece of equipment 'newItem' 
    public void Equip(Equipment newItem) 
    {
        //here we want to define what piece of equipment goes in what slot, (in the equipment stats)
        // so we create a new int slotIndex, and = to (int)newItem.equipSlot;
        //we refer to the method Equip(equipment newItem)
        //we are asking the item from Equipment.cs 'newItem' EquipmentSlot index #
        //Equipment.cs -> head = 0, weapon = 1, Shield = 2.
        //---- JUMP TO switch case: 0 ----
        int slotIndex = (int)newItem.equipSlot;

        //create a variable oldItem = null
        //here we set the variable to reference the old item down below
        Equipment oldItem = null;

        //here we check if the currentEquipment[and its slot index] has an item
        //if it does well then we want to do something
        if (currentEquipment[slotIndex] != null)
        {
            //if there is an old item, as old item is = to the currentEquipment index
            oldItem = currentEquipment[slotIndex];
            //Here we add the old item back into the inventory by calling the method Add from Inventory.cs
            //and we feed it in the Equipment oldItem;
            inventory.Add(oldItem);
            //and we also need to destroy the obj we created on the player itself
            Destroy(currentObj[slotIndex].gameObject);
        }

        //other scripts are subscribed to this callback method see top for more
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        switch (slotIndex)
        {
            //because we want items to apply to different locations i've created a switch statement
            //that runs off slot index so (head = 0) to apply physical items to the player,
            //--Note this script may require revision as issues could occour--
            case 0:
                //Here we are setting the currentEquipment array to [the new slot int]
                //to refer to equipment so we can easily remove the equipment in later code.
                currentEquipment[slotIndex] = newItem;
                //here is how the object spawns in
                GameObject newHandObj = Instantiate<GameObject>(newItem.equipmentObj);
                //where its anchor point/parent obj is
                newHandObj.transform.parent = headPos.transform;
                //making sure its the position of the player not worldspace
                newHandObj.transform.position = headPos.transform.position;
                //making sure its the rotation of the player not worldspace
                newHandObj.transform.rotation = headPos.transform.rotation;
                //once all the details are added in we want to re-write the data to easily replace
                //so we change it to newHandObj, This will be different depending on the case!
                currentObj[slotIndex] = newHandObj;
                break; //for the love of god dont forget to break
            case 1:
                currentEquipment[slotIndex] = newItem;
                GameObject newWeaponObj = Instantiate<GameObject>(newItem.equipmentObj);
                newWeaponObj.transform.parent = weaponPos.transform;
                newWeaponObj.transform.position = weaponPos.transform.position;
                newWeaponObj.transform.rotation = weaponPos.transform.rotation;
                currentObj[slotIndex] = newWeaponObj;
                break;
            //case 2:   //Add this back for sheild
            //    currentEquipment[slotIndex] = newItem;
            //    GameObject newShieldObj = Instantiate<GameObject>(newItem.equipmentObj);
            //    newShieldObj.transform.parent = shieldPos.transform;
            //    newShieldObj.transform.position = shieldPos.transform.position;
            //    newShieldObj.transform.rotation = shieldPos.transform.rotation;
            //    currentObj[slotIndex] = newShieldObj;
            //    break;
            default:
                print("slot index default was called");
                break;
            //   ------  DONT FORGET TO ADD DEFAULT WITH SWITCH STATEMENTS -------
            //nothing should ever be default

        }
    }

    //we take in a int with a slot index
    public void Unequip (int slotIndex)
    {
        //we check if there is any item in the slot
        if (currentEquipment[slotIndex] != null)
        {
            //here we are saying if an obj is in the slot well, destroy (current object [slot index])
            if (currentObj [slotIndex] != null)
            {
                Destroy(currentObj[slotIndex].gameObject);
            }

            //here we get the old item
            Equipment oldItem = currentEquipment[slotIndex];
            //then we add the old item back
            inventory.Add(oldItem);

            //here we now set currentEquipment[index] = to null as we are unequiping everything
            currentEquipment[slotIndex] = null;

            //other scripts are subscribed to this callback method see top for more
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    //here we have created a method UnequipAll as for debug purposes we will have an unequip all key 'U'
    //and because we have multiple pieces of equipment we want to ensure we go through all of them and unequip them

    public void UnequipAll()
    {
        //here we are setting the variable to cycle through each one and
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            //call upon the method taking in the variable i;
            Unequip(i);
        }
    }

    void Update()
    {
        //here is the hot key to unequip all
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();

        //here we have a possible new feature E to active the mouse to pick up objects,
        //look at having a custom mouse image?
        if (Input.GetKeyDown(KeyCode.E) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        } else if (Input.GetKeyDown(KeyCode.E) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
