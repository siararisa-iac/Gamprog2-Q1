using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public ItemData storedItem;
    public Image itemIcon;

    public void AddItem(ItemData item)
    {
        throw new System.NotImplementedException();
    }
}
