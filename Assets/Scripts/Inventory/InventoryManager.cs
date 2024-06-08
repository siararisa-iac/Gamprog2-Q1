using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Player player;
    //For now, this will store information of the Items that can be added to the inventory
    public List<ItemData> itemDatabase;

    //Store all the inventory slots in the scene here
    public List<InventorySlot> inventorySlots;

    //Store all the equipment slots in the scene here
    public List<EquipmentSlot> equipmentSlots;

    //Singleton implementation. Do not change anything within this region.
    #region SingletonImplementation
    private static InventoryManager instance = null;
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "Inventory";
                    instance = go.AddComponent<InventoryManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public void UseItem(ItemData data)
    {
        // TODO
       
        switch (data.type)
        {
            // If the item is a consumable, simply add the attributes of the item to the player.
            case ItemType.Consumable:
                player.AddAttributes(data.attributes);
                break;
            // If it is equippable, 
            case ItemType.Equipabble:
                //Get the equipment slot that matches the item's slot.
                // Set the equipment slot's item as that of the used item
                GetEquipmentSlot(data.slotType).SetItem(data);
                break;
        }
    }

   
    public void AddItem(string itemID)
    {
        //TODO
        //1. Cycle through every item in the database until you find the item with the same id.
        for(int i = 0; i < itemDatabase.Count; i++)
        {
            //Check if the id matches
            if (itemDatabase[i].id == itemID)
            {
                //2. Get the index of the InventorySlot that does not have any Item and set its Item to the Item found
                GetEmptyInventorySlot().SetItem(itemDatabase[i]);
            }
        }
    }

    public void Unequip(ItemData itemData)
    {
        // Check if there is an available inventory slot before removing the item.
        InventorySlot emptySlot = GetEmptyInventorySlot();
        // There is an available slot
        if (emptySlot != null)
        {
            // Make sure to return the equipment to the inventory when there is an available slot.
            emptySlot.SetItem(itemData);
            player.RemoveAttributes(itemData.attributes);
        }
    }

    public int GetEmptyInventorySlotIndex()
    {
        //TODO
        //Check which inventory slot doesn't have an Item and return its index
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].HasItem()) return i;
        }
        // default return
        return -1;
    }

    public InventorySlot GetEmptyInventorySlot()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].HasItem()) return inventorySlots[i];
        }
        return null;
    }

    public int GetEquipmentSlotIndex(EquipmentSlotType type)
    {
        //TODO
        //Check which equipment slot matches the slot type and return its index
        for(int i = 0; i < equipmentSlots.Count; i++)
        {
            if (equipmentSlots[i].type == type) return i;
        }
        return -1;
    }

    public EquipmentSlot GetEquipmentSlot(EquipmentSlotType type)
    {
        //TODO
        //Check which equipment slot matches the slot type and return its index
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if (equipmentSlots[i].type == type) return equipmentSlots[i];
        }
        return null;
    }
}
