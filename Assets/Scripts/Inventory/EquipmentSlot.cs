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
        itemData = data;
        defaultIcon.gameObject.SetActive(false);
        itemIcon.gameObject.SetActive(true);
        itemIcon.sprite = data.icon;
        InventoryManager.Instance.player.AddAttributes(itemData.attributes);
    }

    public void Unequip()
    {
        int availableSlot = InventoryManager.Instance.GetEmptyInventorySlot();
        if (availableSlot == -1) return;
        InventoryManager.Instance.player.RemoveAttributes(itemData.attributes);
        InventoryManager.Instance.AddItem(itemData.id);
        defaultIcon.gameObject.SetActive(true);
        itemIcon.gameObject.SetActive(false);
        itemIcon.sprite = null;
        itemData = null;
    }
}
