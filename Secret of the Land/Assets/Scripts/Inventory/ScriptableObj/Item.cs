using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ItemType
{
    Equipment,
    Consumable,
}

public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int count;
    [TextArea]
    public string itemInfo;
    public ItemType itemType;
    public int value;

    void OnDisable()
    {
        count = 0;
    }
}
