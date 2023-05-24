using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string id;
    public Sprite icon;
    public ItemType type;
    public EquipmentSlotType slotType;
    public List<Attribute> attributes;
}

public enum ItemType
{
    Consumable,
    Equipabble, 
}
public enum EquipmentSlotType
{
    None,
    Head,
    Armor,
    Boot,
    Weapon1,
    Weapon2
}

[System.Serializable]
public class Attribute
{
    public AttributeType type;
    public float value;

    public Attribute(AttributeType type, float value)
    {
        this.type = type;
        this.value = value;
    }
}

public enum AttributeType
{
    Strength,
    Defense,
    HP
}