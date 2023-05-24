using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string id;
    public Sprite icon;
    public ItemType type;
    public Status status;
}

public enum ItemType
{
    Consumable,
    Equipabble, 
}

[System.Serializable]
public struct Status
{
    public int Strength;
    public int Agility;
    public int Vitality;
}