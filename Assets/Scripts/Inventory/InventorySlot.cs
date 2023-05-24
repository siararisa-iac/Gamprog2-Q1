using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private ItemData itemData;
    public Image itemIcon;

    public void SetItem(ItemData data)
    {
        // TODO
        // Set the item data the and icons here
    }

    public void UseItem()
    {
        InventoryManager.Instance.UseItem(itemData);
        // TODO
        // Reset the item data and the icons here
    }

    public bool HasItem()
    {
        return itemData != null;
    }
}
