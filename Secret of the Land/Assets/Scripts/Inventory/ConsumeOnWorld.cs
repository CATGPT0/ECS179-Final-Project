using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class ConsumeOnWorld : ItemOnWorld
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
            //item.onItemUse += PlayerController.Instance.Player.ChangeHealth;
        }
        else if (item.count < 9)
        {
            item.count++;
            InventoryManager.Refresh(item);
            //item.onItemUse += PlayerController.Instance.Player.ChangeHealth;
        }
    }
}
