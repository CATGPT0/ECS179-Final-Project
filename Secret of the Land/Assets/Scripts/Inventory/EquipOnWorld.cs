using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipOnWorld : ItemOnWorld
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void AddItem()
    {
        if (!inventory.items.Contains(item))
        {
            item.count = 1;
            inventory.items.Add(item);
            InventoryManager.CreateNewItem(item);
        }
    }
}
