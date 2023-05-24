using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private ItemData storedItem;
    public Image itemIcon;

    public void AddItem(ItemData item)
    {
        storedItem = item;
        itemIcon.enabled = true;
        itemIcon.sprite = item.icon;
    }

    public void UseItem()
    {
        if (storedItem == null) return;
        InventoryManager.Instance.UseItem(storedItem);
        itemIcon.enabled = false;
        storedItem = null;
    }

    public bool HasItem()
    {
        return storedItem != null;
    }
}
