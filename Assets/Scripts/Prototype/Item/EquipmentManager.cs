using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject headPos, weaponPos; // add shieldPos
    Equipment[] currentEquipment;
    GameObject[] currentObj;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentObj = new GameObject[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            Destroy(currentObj[slotIndex].gameObject);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        switch (slotIndex)
        {
            case 0:
                currentEquipment[slotIndex] = newItem;
                GameObject newHandObj = Instantiate<GameObject>(newItem.equipmentObj);
                newHandObj.transform.parent = headPos.transform;
                newHandObj.transform.position = headPos.transform.position;
                newHandObj.transform.rotation = headPos.transform.rotation;
                currentObj[slotIndex] = newHandObj;
                break;
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

        }
    }

    public void Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentObj [slotIndex] != null)
            {
                Destroy(currentObj[slotIndex].gameObject);
            }

            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();

        if (Input.GetKeyDown(KeyCode.E) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        } else if (Input.GetKeyDown(KeyCode.E) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
