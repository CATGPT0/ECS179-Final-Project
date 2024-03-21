using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class GameItem
{
    public Item item;
    public Action<int> onItemUse;
    public Action<int> onItemEquip;
    public Action<int> onItemTeardown;

    public GameItem(Item item)
    {
        this.item = item;

        if (item.itemType == ItemType.Consumable)
        {
            if (item.itemName == "HealthPotion")
            {
                onItemUse += PlayerController.Instance.Player.ChangeHealth;
            }
            else if (item.itemName == "ManaPotion")
            {
                onItemUse += PlayerController.Instance.Player.ChangeMana;
            }
            
        }
        else if (item.itemType == ItemType.Equipment)
        {
            onItemEquip += PlayerController.Instance.Player.ChangeAttackPower;
            onItemTeardown += PlayerController.Instance.Player.ChangeAttackPower;
        }
        // onItemUse += PlayerController.Instance.Player.ChangeHealth;
        // onItemEquip += PlayerController.Instance.Player.ChangeAttackPower;
        // onItemTeardown += PlayerController.Instance.Player.ChangeAttackPower;
    }
}