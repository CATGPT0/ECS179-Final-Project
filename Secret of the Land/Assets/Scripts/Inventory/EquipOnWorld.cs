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
        GameItem thisItem = new GameItem(item);
        if (thisItem.item.count == 0)
        {
            thisItem.item.count = 1;
            inventory.items.Add(thisItem);
            InventoryManager.CreateNewItem(thisItem);
        }
    }
}
