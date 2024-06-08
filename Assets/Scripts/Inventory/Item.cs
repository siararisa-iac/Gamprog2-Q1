using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public override void Interact()
    {
        // TODO: Add the item to the inventory. 
        if (InventoryManager.Instance.AddItem(id))
        {
            // Make sure to destroy the prefab once the item is collected 
            Destroy(this.gameObject);
        }
        
    }
}
