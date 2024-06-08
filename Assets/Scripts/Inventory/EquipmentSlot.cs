using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Image defaultIcon;
    [SerializeField] private Image itemIcon;
    public EquipmentSlotType type;

    private ItemData itemData;

    public void SetItem(ItemData data)
    {
        // TODO
        // Set the item data the and icons here
        itemData = data;
        defaultIcon.gameObject.SetActive(false);
        itemIcon.gameObject.SetActive(true);
        itemIcon.sprite = data.icon;
        // Make sure to apply the attributes once an item is equipped
        InventoryManager.Instance.player.AddAttributes(itemData.attributes);
    }

    public void Unequip()
    {
        // Unequip the item first before resetting its value
        InventoryManager.Instance.Unequip(itemData);
        // Reset the item data and icons here
        itemData = null;
        defaultIcon.gameObject.SetActive(true);
        itemIcon.gameObject.SetActive(false);
        itemIcon.sprite = null;
    }
}
