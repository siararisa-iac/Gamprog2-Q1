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
        if(data.type == ItemType.Consumable)
        {
            player.AddAttributes(data.attributes);
        }
        else if(data.type == ItemType.Equipabble)
        {
            equipmentSlots[GetEquipmentSlot(data.slotType)].SetItem(data);
        }
    }

    public void AddItem(string itemID)
    {
        //TODO
        //1. Cycle through every item in the database until you find the item with the same id
        for(int i = 0; i < itemDatabase.Count; i++)
        {
            if(itemDatabase[i].id == itemID)
            {
                Debug.Log(GetEmptyInventorySlot());
                //2. Get the index of the InventorySlot that does not have any Item yet and set its Item to the Item found
                inventorySlots[GetEmptyInventorySlot()].AddItem(itemDatabase[i]);
            }
        }
        
    }

    public int GetEmptyInventorySlot()
    {
        //Check which slot doesn't have an Item and return its index
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].HasItem())
            {
                return i;
            }  
        }
        return -1;
    }

    public int GetEquipmentSlot(EquipmentSlotType type)
    {
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if(equipmentSlots[i].type == type)
            {
                return i;
            }
        }
        return -1;
    }
}
